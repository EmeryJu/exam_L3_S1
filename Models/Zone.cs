using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public class Zone
    {
        public int Id { get; set; }

        [Required]
        public required string Nom { get; set; }

        public required string Quartiers { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrixLivraison { get; set; }

        // Navigation
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}