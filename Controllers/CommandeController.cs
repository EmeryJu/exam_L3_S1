using BrasilBurger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrasilBurger.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommandeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Commande/Commander?burgerId=1
        public async Task<IActionResult> Commander(int? burgerId, int? menuId)
        {
            var model = new CommanderViewModel();

            if (burgerId.HasValue)
            {
                model.Burger = await _context.Burgers.FindAsync(burgerId.Value);
                model.Type = "Burger";
            }
            else if (menuId.HasValue)
            {
                model.Menu = await _context.Menus.Include(m => m.Burger).Include(m => m.MenuComplements).ThenInclude(mc => mc.Complement).FirstOrDefaultAsync(m => m.Id == menuId.Value);
                model.Type = "Menu";
            }

            model.Complements = await _context.Complements.ToListAsync();
            model.Zones = await _context.Zones.ToListAsync();

            return View(model);
        }

        // POST: Commande/Commander
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Commander(CommanderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create client if not exists, but for simplicity, assume client is logged in or create new
                var client = new Client
                {
                    Nom = model.ClientNom,
                    Prenom = model.ClientPrenom,
                    Telephone = model.ClientTelephone,
                    Email = model.ClientEmail,
                    Password = "temp" // hash later
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                var commande = new Commande
                {
                    ClientId = client.Id,
                    Type = Enum.Parse<TypeCommande>(model.TypeCommande!),
                    ZoneId = model.ZoneId,
                    Etat = EtatCommande.EnCours
                };

                var item = new CommandeItem();
                decimal itemPrix = 0;
                if (model.Burger != null)
                {
                    item.BurgerId = model.Burger.Id;
                    itemPrix += model.Burger.Prix;
                }
                else if (model.Menu != null)
                {
                    item.MenuId = model.Menu.Id;
                    itemPrix += model.Menu.Prix;
                }

                // Add selected complements
                if (model.SelectedComplements != null)
                {
                    foreach (var compId in model.SelectedComplements)
                    {
                        var comp = await _context.Complements.FindAsync(compId);
                        if (comp != null)
                        {
                            item.Complements.Add(new CommandeItemComplement { CommandeItemId = item.Id, CommandeItem = item, ComplementId = compId, Complement = comp });
                            itemPrix += comp.Prix;
                        }
                    }
                }

                commande.Items.Add(item);
                commande.Total = itemPrix + (commande.Zone?.PrixLivraison ?? 0);

                _context.Commandes.Add(commande);
                await _context.SaveChangesAsync();

                // Create paiement
                var paiement = new Paiement
                {
                    CommandeId = commande.Id,
                    Montant = commande.Total,
                    Methode = Enum.Parse<MethodePaiement>(model.MethodePaiement!)
                };
                _context.Paiements.Add(paiement);
                await _context.SaveChangesAsync();

                return RedirectToAction("Confirmation", new { id = commande.Id });
            }

            // Reload data
            model.Complements = await _context.Complements.ToListAsync();
            model.Zones = await _context.Zones.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var commande = await _context.Commandes.Include(c => c.Client).Include(c => c.Items).ThenInclude(i => i.Burger).Include(c => c.Items).ThenInclude(i => i.Menu).FirstOrDefaultAsync(c => c.Id == id);
            return View(commande);
        }
    }

    public class CommanderViewModel
    {
        public Burger? Burger { get; set; }
        public Menu? Menu { get; set; }
        public string? Type { get; set; }
        public List<Complement>? Complements { get; set; }
        public int[]? SelectedComplements { get; set; }
        public List<Zone>? Zones { get; set; }
        public int ZoneId { get; set; }
        public string? TypeCommande { get; set; } // SurPlace, Recuperer, Livrer
        public string? MethodePaiement { get; set; } // Wave, OM
        public string? ClientNom { get; set; }
        public string? ClientPrenom { get; set; }
        public string? ClientTelephone { get; set; }
        public string? ClientEmail { get; set; }
    }
}