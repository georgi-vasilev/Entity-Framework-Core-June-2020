namespace RecipeApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using RecipeApp.Data.Models;
    using RecipeApp.EntityConfigurations;

    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext()
        {
        }

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Recipe> Ingredients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=./;Database=Recipes;Integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RecipeConfigurations());
            //modelBuilder.Entity<Employee>()
            //    .HasOne(x => x.Address)
            //    .WithOne(x => x.Employee)
            //    .OnDelete(DeleteBehavior.Restrict); // 1 - 1 relation

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(x => new { x.IngredientId, x.RecipeId }); //creates composite key
        }
    }
}
