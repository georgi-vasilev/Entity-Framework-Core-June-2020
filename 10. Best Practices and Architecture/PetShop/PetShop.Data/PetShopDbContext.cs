namespace PetShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Common;
    using PetShop.Models;

    public class PetShopDbContext : DbContext
    {
        public PetShopDbContext()
        {
        }

        public PetShopDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Breed> Breeds { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientProduct> ClientProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfiguration.DefaultConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasAlternateKey(p => p.Name);

            modelBuilder.Entity<ClientProduct>()
                .HasKey(cp => new { cp.ClientId, cp.ProductId });

            modelBuilder.Entity<Order>()
                .Ignore(o => o.TotalPrice);
        }
    }
}
