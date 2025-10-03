using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickTix.Data;
using System.Diagnostics;

namespace QuickTix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuickTixContext _context;
        public HomeController(ILogger<HomeController> logger, QuickTixContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var quickTixContext = _context.Listing.Include(l => l.Categories).Include(l => l.Owners);
            return View(await quickTixContext.ToListAsync());
        }
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
    }
}
