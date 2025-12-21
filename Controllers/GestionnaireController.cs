using BrasilBurger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrasilBurger.Controllers
{
    public class GestionnaireController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GestionnaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gestionnaire
        public IActionResult Index()
        {
            return View();
        }

        // GET: Gestionnaire/Commandes
        public async Task<IActionResult> Commandes(string filter = "all", DateTime? date = null, string etat = "all", int? clientId = null)
        {
            var query = _context.Commandes.Include(c => c.Client).Include(c => c.Items).AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(c => c.Date.Date == date.Value.Date);
            }

            if (etat != "all")
            {
                var etatEnum = Enum.Parse<EtatCommande>(etat);
                query = query.Where(c => c.Etat == etatEnum);
            }

            if (clientId.HasValue)
            {
                query = query.Where(c => c.ClientId == clientId.Value);
            }

            if (filter == "burgers")
            {
                query = query.Where(c => c.Items.Any(i => i.BurgerId.HasValue));
            }
            else if (filter == "menus")
            {
                query = query.Where(c => c.Items.Any(i => i.MenuId.HasValue));
            }

            return View(await query.ToListAsync());
        }

        // POST: Gestionnaire/AnnulerCommande/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnnulerCommande(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);
            if (commande != null)
            {
                commande.Etat = EtatCommande.Annule;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Commandes));
        }

        // POST: Gestionnaire/TerminerCommande/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TerminerCommande(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);
            if (commande != null)
            {
                commande.Etat = EtatCommande.Termine;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Commandes));
        }

        // GET: Gestionnaire/Stats
        public async Task<IActionResult> Stats()
        {
            var today = DateTime.Today;

            var model = new
            {
                CommandesEnCours = await _context.Commandes.CountAsync(c => c.Date.Date == today && c.Etat == EtatCommande.EnCours),
                CommandesValidees = await _context.Commandes.CountAsync(c => c.Date.Date == today && c.Etat == EtatCommande.Pret),
                RecettesJournalieres = await _context.Paiements.Where(p => p.Date.Date == today).SumAsync(p => p.Montant),
                BurgersPopulaires = await _context.CommandeItems
                    .Include(ci => ci.Burger)
                    .Where(ci => ci.BurgerId.HasValue && ci.Commande.Date.Date == today)
                    .GroupBy(ci => ci.Burger!.Nom)
                    .Select(g => new { Nom = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .FirstOrDefaultAsync(),
                CommandesAnnulees = await _context.Commandes.CountAsync(c => c.Date.Date == today && c.Etat == EtatCommande.Annule)
            };

            return View(model);
        }
    }
}