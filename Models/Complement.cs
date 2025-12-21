using System.ComponentModel.DataAnnotations;

namespace BrasilBurger.Models
{
    public class Complement
    {
        public int Id { get; set; }

        [Required]
        public required string Nom { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Prix { get; set; }

        public required string Image { get; set; }

        public bool Archive { get; set; } = false;
    }
}