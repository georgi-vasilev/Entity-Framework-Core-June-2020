using RecepiesSystem_CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace RecepiesSystem_CodeFirst
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new RecipesDbContext();
            db.Database.Migrate();
        }
    }
}
