namespace RecepiesSystem_CodeFirst.Models
{
    using Microsoft.EntityFrameworkCore;

    public class RecipesDbContext : DbContext
    {
        public RecipesDbContext()
        {

        }

        public RecipesDbContext(DbContextOptions<RecipesDbContext> options) 
            : base (options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
