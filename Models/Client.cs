using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public required string Nom { get; set; }

        [Required]
        public required string Prenom { get; set; }

        [Required]
        [Phone]
        public required string Telephone { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; } // In real app, hash it

        // Navigation
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}