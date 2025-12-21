using BrasilBurger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrasilBurger.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catalogue
        public async Task<IActionResult> Index(string filter = "all")
        {
            ViewData["Filter"] = filter;
            if (filter == "burgers")
            {
                return View("Burgers", await _context.Burgers.ToListAsync());
            }
            else if (filter == "menus")
            {
                return View("Menus", await _context.Menus.Include(m => m.Burger).Include(m => m.MenuComplements).ThenInclude(mc => mc.Complement).ToListAsync());
            }
            else
            {
                var model = new
                {
                    Burgers = await _context.Burgers.ToListAsync(),
                    Menus = await _context.Menus.Include(m => m.Burger).Include(m => m.MenuComplements).ThenInclude(mc => mc.Complement).ToListAsync()
                };
                return View(model);
            }
        }

        // GET: Catalogue/DetailsBurger/5
        public async Task<IActionResult> DetailsBurger(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _context.Burgers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burger == null)
            {
                return NotFound();
            }

            return View(burger);
        }

        // GET: Catalogue/DetailsMenu/5
        public async Task<IActionResult> DetailsMenu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                .ThenInclude(mc => mc.Complement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }
    }
}