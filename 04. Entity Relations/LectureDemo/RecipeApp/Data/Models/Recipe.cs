namespace RecipeApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Recipe
    {
        public Recipe()
        {
            this.Ingredients = new HashSet<RecipeIngredient>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] 
        public string Name { get; set; }

        [NotMapped] //means that it will be hidden from the db.
        public string Description => $"${this.Name} ({this.CookingTime})";

        public TimeSpan? CookingTime { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }

    }
}
