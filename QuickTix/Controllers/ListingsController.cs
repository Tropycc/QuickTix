using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickTix.Data;
using QuickTix.Models;

namespace QuickTix.Controllers
{
    [Authorize]
    public class ListingsController : Controller
    {
        private readonly QuickTixContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ListingsController(QuickTixContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            var listings = _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners);
            return View(await listings.ToListAsync());
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var listing = await _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners)
                .FirstOrDefaultAsync(m => m.ListingId == id);

            if (listing == null) return NotFound();

            return View(listing);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name");
            return View();
        }

        // POST: Listings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId,CategoryId,OwnerId,Title,Description,Location,ListingDate,Photo")] Listing listing)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns and return view
                ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", listing.CategoryId);
                ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", listing.OwnerId);
                return View(listing);
            }

            // Handle image upload
            if (listing.Photo != null && listing.Photo.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(listing.Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await listing.Photo.CopyToAsync(fileStream);
                }

                listing.PhotoFileName = uniqueFileName;
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

        // POST: Listings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    [Bind("ListingId,CategoryId,OwnerId,Title,Description,Location,ListingDate,Photo,PhotoFileName")] Listing listing)
        {
            if (id != listing.ListingId)
                return NotFound();

            var existingListing = await _context.Listing.AsNoTracking()
                .FirstOrDefaultAsync(l => l.ListingId == id);

            if (existingListing == null)
                return NotFound();

            listing.Photo ??= null;

            if (ModelState.IsValid)
            {
                try
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    Directory.CreateDirectory(uploadsFolder);

                    // If a new photo is uploaded
                    if (listing.Photo != null && listing.Photo.Length > 0)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(listing.Photo.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await listing.Photo.CopyToAsync(fileStream);
                        }

                        // Delete old photo if exists
                        if (!string.IsNullOrEmpty(existingListing.PhotoFileName))
                        {
                            string oldFile = Path.Combine(uploadsFolder, existingListing.PhotoFileName);
                            if (System.IO.File.Exists(oldFile))
                                System.IO.File.Delete(oldFile);
                        }

                        listing.PhotoFileName = uniqueFileName;
                    }
                    else
                    {
                        // No new photo uploaded — preserve existing or none
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

            // Repopulate dropdowns for re-rendered view
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", listing.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", listing.OwnerId);
            listing.ExistingPhotoPath = existingListing.PhotoFileName;

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
                if (!string.IsNullOrEmpty(listing.PhotoFileName))
                {
                    string photoPath = Path.Combine(_hostEnvironment.WebRootPath, "images", listing.PhotoFileName);
                    if (System.IO.File.Exists(photoPath))
                        System.IO.File.Delete(photoPath);
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