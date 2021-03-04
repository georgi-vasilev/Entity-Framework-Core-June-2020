namespace PetShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using PetShop.Models.Enumerations;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ProductType ProductType { get; set; }

        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }

    }
}
