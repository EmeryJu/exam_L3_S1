using Microsoft.EntityFrameworkCore;

namespace BrasilBurger.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Burger> Burgers { get; set; }

        public DbSet<Complement> Complements { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuComplement> MenuComplements { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Commande> Commandes { get; set; }

        public DbSet<CommandeItem> CommandeItems { get; set; }

        public DbSet<CommandeItemComplement> CommandeItemComplements { get; set; }

        public DbSet<Paiement> Paiements { get; set; }

        public DbSet<Zone> Zones { get; set; }

        public DbSet<Livreur> Livreurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships if needed
            // For example, MenuComplement
            modelBuilder.Entity<MenuComplement>()
                .HasKey(mc => new { mc.MenuId, mc.ComplementId });

            modelBuilder.Entity<MenuComplement>()
                .HasOne(mc => mc.Menu)
                .WithMany(m => m.MenuComplements)
                .HasForeignKey(mc => mc.MenuId);

            modelBuilder.Entity<MenuComplement>()
                .HasOne(mc => mc.Complement)
                .WithMany()
                .HasForeignKey(mc => mc.ComplementId);

            // Similarly for CommandeItemComplement
            modelBuilder.Entity<CommandeItemComplement>()
                .HasKey(cic => new { cic.CommandeItemId, cic.ComplementId });

            modelBuilder.Entity<CommandeItemComplement>()
                .HasOne(cic => cic.CommandeItem)
                .WithMany(ci => ci.Complements)
                .HasForeignKey(cic => cic.CommandeItemId);

            modelBuilder.Entity<CommandeItemComplement>()
                .HasOne(cic => cic.Complement)
                .WithMany()
                .HasForeignKey(cic => cic.ComplementId);
        }
    }
}