using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickTix.Data;
using QuickTix.Models;
using System.Composition;

namespace QuickTix.Controllers
{
    [Authorize]
    public class ListingsController : Controller
    {
        private readonly QuickTixContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly BlobContainerClient _containerClient;

        public ListingsController(QuickTixContext context, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;

            var connectionString = _configuration["AzureStorage"];

            // DEBUG: Check what the connection string actually is
            Console.WriteLine("AzureStorage connection string: " + connectionString);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("AzureStorage secret not found. Make sure user secrets are configured correctly.");
            }

            var containerName = "uploads";
            _containerClient = new BlobContainerClient(connectionString, containerName);
            _containerClient.CreateIfNotExists(PublicAccessType.Blob);
            _containerClient.SetAccessPolicy(PublicAccessType.Blob);
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            var listings = _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners);

            ViewBag.BlobBaseUrl = "https://nscc0511519inet.blob.core.windows.net/uploads";
            return View(await listings.ToListAsync());
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            // Load the listing with related Category and Owner
            var listing = await _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners)
                .FirstOrDefaultAsync(m => m.ListingId == id);

            if (listing == null)
                return NotFound();

            // Load all purchases for this listing
            var purchases = await _context.Purchase
                .Where(p => p.ListingId == listing.ListingId)
                .OrderByDescending(p => p.PurchaseDate)
                .ToListAsync();

            // Pass purchases to the view via ViewBag
            ViewBag.Purchases = purchases;

            return View(listing);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId,CategoryId,OwnerId,Title,Description,Location,ListingDate,TicketPrice,Photo")] Listing listing)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", listing.CategoryId);
                ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", listing.OwnerId);
                return View(listing);
            }

            if (listing.Photo != null && listing.Photo.Length > 0)
            {
                var fileExt = Path.GetExtension(listing.Photo.FileName);
                string blobName = Guid.NewGuid().ToString() + fileExt;

                var blobClient = _containerClient.GetBlobClient(blobName);

                using (var stream = listing.Photo.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = listing.Photo.ContentType });
                }

                listing.PhotoFileName = blobName;
            }

            _context.Add(listing);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET: Listings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var listing = await _context.Listing.FindAsync(id);
            if (listing == null) return NotFound();

            // Show current photo in the edit view
            listing.ExistingPhotoPath = listing.PhotoFileName;

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", listing.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", listing.OwnerId);
            return View(listing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    [Bind("ListingId,CategoryId,OwnerId,Title,Description,Location,ListingDate,TicketPrice,Photo,PhotoFileName")] Listing listing)
        {
            if (id != listing.ListingId)
                return NotFound();

            var existingListing = await _context.Listing.AsNoTracking()
                .FirstOrDefaultAsync(l => l.ListingId == id);

            if (existingListing == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // If a new photo was uploaded
                    if (listing.Photo != null && listing.Photo.Length > 0)
                    {
                        string blobName = Guid.NewGuid().ToString() + Path.GetExtension(listing.Photo.FileName);
                        var blobClient = _containerClient.GetBlobClient(blobName);

                        using (var stream = listing.Photo.OpenReadStream())
                        {
                            await blobClient.UploadAsync(stream, new BlobHttpHeaders
                            {
                                ContentType = listing.Photo.ContentType
                            });
                        }

                        if (!string.IsNullOrEmpty(existingListing.PhotoFileName))
                        {
                            var oldBlob = _containerClient.GetBlobClient(existingListing.PhotoFileName);
                            await oldBlob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                        }

                        listing.PhotoFileName = blobName;
                    }
                    else
                    {
                        listing.PhotoFileName = existingListing.PhotoFileName;
                    }

                    _context.Update(listing);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving edit: {ex.Message}");
                    ModelState.AddModelError("", "There was an issue saving your edit. Please try again.");
                }
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", listing.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", listing.OwnerId);

            return View(listing);
        }

        // GET: Listings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var listing = await _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners)
                .FirstOrDefaultAsync(m => m.ListingId == id);

            if (listing == null) return NotFound();

            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listing = await _context.Listing.FindAsync(id);
            if (listing != null)
            {
                // Delete photo from Azure Blob Storage
                if (!string.IsNullOrEmpty(listing.PhotoFileName))
                {
                    var blob = _containerClient.GetBlobClient(listing.PhotoFileName);
                    await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                }

                _context.Listing.Remove(listing);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        private bool ListingExists(int id)
        {
            return _context.Listing.Any(e => e.ListingId == id);
        }
    }
}