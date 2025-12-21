using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public enum MethodePaiement
    {
        Wave,
        OM
    }

    public class Paiement
    {
        public int Id { get; set; }

        [Required]
        public int CommandeId { get; set; }

        public Commande? Commande { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Montant { get; set; }

        [Required]
        public MethodePaiement Methode { get; set; }
    }
}