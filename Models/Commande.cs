using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public enum TypeCommande
    {
        SurPlace,
        Recuperer,
        Livrer
    }

    public enum EtatCommande
    {
        EnCours,
        Pret,
        Termine,
        Annule
    }

    public class Commande
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public TypeCommande Type { get; set; }

        public EtatCommande Etat { get; set; } = EtatCommande.EnCours;

        public decimal Total { get; set; }

        public int? ZoneId { get; set; }

        public Zone? Zone { get; set; }

        public int? LivreurId { get; set; }

        public Livreur? Livreur { get; set; }

        // Navigation
        public ICollection<CommandeItem> Items { get; set; } = new List<CommandeItem>();

        public Paiement? Paiement { get; set; }
    }
}