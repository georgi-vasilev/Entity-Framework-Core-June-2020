namespace PetShop.Service.Models.Products.OutputModels
{
    using PetShop.Models.Enumerations;

    public class ListAllProductsServiceModel
    {
        public string Name { get; set; }

        public string ProductType { get; set; }

        public decimal Price { get; set; }
    }
}
