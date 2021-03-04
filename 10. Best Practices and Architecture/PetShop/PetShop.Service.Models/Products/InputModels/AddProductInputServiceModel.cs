namespace PetShop.Service.Models.Products.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddProductInputServiceModel
    {
        [Required]
        public string Name { get; set; }

        public string ProductType { get; set; }

        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }
    }
}
