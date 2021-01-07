using RecepiesSystem_CodeFirst.Models;

namespace RecepiesSystem_CodeFirst
{
    public class RecipesService
    {
        private RecipesDbContext db;

        public RecipesService()
        {
            this.db = new RecipesDbContext();
        }

    }
}
