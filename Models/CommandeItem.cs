using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public class CommandeItem
    {
        public int Id { get; set; }

        [Required]
        public int CommandeId { get; set; }

        public Commande? Commande { get; set; }

        public int? BurgerId { get; set; }

        public Burger? Burger { get; set; }

        public int? MenuId { get; set; }

        public Menu? Menu { get; set; }

        // Complements for the item
        public ICollection<CommandeItemComplement> Complements { get; set; } = new List<CommandeItemComplement>();

        // Prix calculated
        public decimal Prix => (Burger?.Prix ?? 0) + (Menu?.Prix ?? 0) + Complements.Sum(c => c.Complement?.Prix ?? 0);
    }

    public class CommandeItemComplement
    {
        public int Id { get; set; }

        public int CommandeItemId { get; set; }

        public CommandeItem? CommandeItem { get; set; }

        public int ComplementId { get; set; }

        public Complement? Complement { get; set; }
    }
}