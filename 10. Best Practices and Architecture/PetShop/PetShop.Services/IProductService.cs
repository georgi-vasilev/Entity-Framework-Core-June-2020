namespace PetShop.Services
{
    using System.Collections.Generic;
    using PetShop.Service.Models.Products.InputModels;
    using PetShop.Service.Models.Products.OutputModels;

    public interface IProductService
    {
        void AddProduct(AddProductInputServiceModel model);

        ICollection<ListAllProductsServiceModel> GetAll();

        ICollection<ListAllProductsByProductTypeServiceModel> ListAllByProductType(string type);

        ICollection<ListAllProductsByNameServiceModel> SerachByName(string name, bool caseSensitive);

        bool RemoveById(string id);

        bool RemoveByName(string name);

        void EditProduct(string id, EditProductInputServiceModel model);
    }
}
