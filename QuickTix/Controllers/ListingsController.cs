using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickTix.Data;
using QuickTix.Models;

namespace QuickTix.Controllers
{
    public class ListingsController : Controller
    {
        private readonly QuickTixContext _context;

        public ListingsController(QuickTixContext context)
        {
            _context = context;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            var quickTixContext = _context.Listing.Include(l => l.Categories).Include(l => l.Owners);
            return View(await quickTixContext.ToListAsync());
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners)
                .FirstOrDefaultAsync(m => m.ListingId == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId");
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId");
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId,CategoryId,OwnerId,Title,Description,Category,Location,Owner,ListingDate")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", listing.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId", listing.OwnerId);
            return View(listing);
        }

        // GET: Listings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listing.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", listing.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId", listing.OwnerId);
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListingId,CategoryId,OwnerId,Title,Description,Category,Location,Owner,ListingDate")] Listing listing)
        {
            if (id != listing.ListingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingExists(listing.ListingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", listing.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "OwnerId", listing.OwnerId);
            return View(listing);
        }

        // GET: Listings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listing
                .Include(l => l.Categories)
                .Include(l => l.Owners)
                .FirstOrDefaultAsync(m => m.ListingId == id);
            if (listing == null)
            {
                return NotFound();
            }

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
                _context.Listing.Remove(listing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingExists(int id)
        {
            return _context.Listing.Any(e => e.ListingId == id);
        }
    }
}
