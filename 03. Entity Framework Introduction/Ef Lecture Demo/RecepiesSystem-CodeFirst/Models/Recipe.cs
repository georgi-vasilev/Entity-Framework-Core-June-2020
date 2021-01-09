namespace RecepiesSystem_CodeFirst.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        //public string Description { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
