using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTix.Data;
using QuickTix.Models;

namespace QuickTix.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly QuickTixContext _context;

        public PurchasesController(QuickTixContext context)
        {
            _context = context;
        }

        // GET: Purchase/Create/{listingId}
        public async Task<IActionResult> Create(int listingId)
        {
            var listing = await _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners)
                .FirstOrDefaultAsync(l => l.ListingId == listingId);

            if (listing == null)
                return NotFound();

            var purchase = new Purchase
            {
                ListingId = listingId
            };

            return View(purchase);
        }

        // POST: Purchase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.PurchaseDate = DateTime.UtcNow;

                _context.Purchase.Add(purchase);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Confirmation), new { id = purchase.PurchaseId });
            }

            return View(purchase);
        }

        // GET: Purchase/Confirmation/{id}
        public async Task<IActionResult> Confirmation(int id)
        {
            var purchase = await _context.Purchase
                .Include(p => p.Listing)
                    .ThenInclude(l => l.Categories)
                .Include(p => p.Listing)
                    .ThenInclude(l => l.Owners)
                .FirstOrDefaultAsync(p => p.PurchaseId == id);

            if (purchase == null)
                return NotFound();

            return View(purchase);
        }

        // GET: Purchase (Admin View)
        public async Task<IActionResult> Index()
        {
            var purchases = await _context.Purchase
                .Include(p => p.Listing)
                .ToListAsync();

            return View(purchases);
        }
    }
}