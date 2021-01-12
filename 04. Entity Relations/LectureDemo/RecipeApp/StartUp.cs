namespace RecipeApp
{
    using System;
    using System.Linq;
    using RecipeApp.Data;
    using RecipeApp.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            //var db = new RecipeDbContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //var recipe = new Recipe
            //{
            //    Name = "Musaka",
            //    CookingTime = new TimeSpan(2, 3, 4),
            //    Ingredients =
            //    {
            //        new RecipeIngredient
            //        {
            //            Ingredient = new Ingredient { Name = "Potato" },
            //            Quantity = 2000,
            //        },
            //        new RecipeIngredient
            //        {
            //            Ingredient = new Ingredient { Name = "Meat" },
            //            Quantity = 1000,
            //        }
            //    }
            //};

            //db.Recipes.Add(recipe);
            //db.SaveChanges();
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(CalculateSum(n));
        }

        private static int CalculateSum(int n)
        {
            if (n <= 3)
            {
                return 2 * n;
            }

            return 3 * CalculateSum(n - 3) + 4 * CalculateSum(n - 2) - 7 * CalculateSum(n - 1);

        }
    }
}
