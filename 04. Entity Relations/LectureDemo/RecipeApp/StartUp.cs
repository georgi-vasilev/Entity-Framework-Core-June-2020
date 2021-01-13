namespace RecipeApp
{
    using System;
    using RecipeApp.Data;
    using RecipeApp.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            var db = new RecipeDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var recipe = new Recipe
            {
                Name = "Musaka",
                CookingTime = new TimeSpan(2, 3, 4),
                Ingredients =
                {
                    new RecipeIngredient
                    {
                        Ingredient = new Ingredient { Name = "Potato" },
                        Quantity = 2000,
                    },
                    new RecipeIngredient
                    {
                        Ingredient = new Ingredient { Name = "Meat" },
                        Quantity = 1000,
                    }
                }
            };

            db.Recipes.Add(recipe);
            db.SaveChanges();
        }
    }
}
