﻿namespace RecepiesSystem_CodeFirst.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
