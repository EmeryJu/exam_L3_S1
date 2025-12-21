using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Required]
        public required string Nom { get; set; }

        public required string Image { get; set; }

        [Required]
        public int BurgerId { get; set; }

        public Burger? Burger { get; set; }

        // For simplicity, assume menu includes boisson and frites as complements
        public ICollection<MenuComplement> MenuComplements { get; set; } = new List<MenuComplement>();

        // Prix calculated as sum
        public decimal Prix => Burger?.Prix + MenuComplements.Sum(mc => mc.Complement?.Prix ?? 0) ?? 0;

        public bool Archive { get; set; } = false;
    }

    public class MenuComplement
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        public Menu? Menu { get; set; }

        public int ComplementId { get; set; }

        public Complement? Complement { get; set; }
    }
}