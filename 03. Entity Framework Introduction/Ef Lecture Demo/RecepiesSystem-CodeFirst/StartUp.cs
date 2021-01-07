using System;
using System.Linq;
using System.Collections.Generic;
using RecepiesSystem_CodeFirst.Models;

namespace RecepiesSystem_CodeFirst
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new RecipesDbContext();
            db.Database.EnsureCreated();

            //cascade insert
            //for (int i = 0; i < 1000; i++)
            //{
            //    db.Recipes.Add(new Recipe {
            //        Name = i.ToString(),
            //        Ingredients = new List<Ingredient>
            //        {
            //            new Ingredient { Name = "Meat", Amount = 500},
            //            new Ingredient { Name = "Potatoes", Amount = 400}
            //        }
            //    });
            //}

            //db.SaveChanges();
        }
    }
}
