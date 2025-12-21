using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public class Livreur
    {
        public int Id { get; set; }

        [Required]
        public required string Nom { get; set; }

        [Required]
        public required string Prenom { get; set; }

        [Required]
        [Phone]
        public required string Telephone { get; set; }

        // Navigation
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}