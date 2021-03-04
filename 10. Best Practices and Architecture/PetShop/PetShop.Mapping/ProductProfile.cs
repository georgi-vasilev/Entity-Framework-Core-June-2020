namespace PetShop.Mapping
{
    using System;

    using AutoMapper;

    using PetShop.Models;
    using PetShop.Models.Enumerations;
    using PetShop.Service.Models.Products.InputModels;
    using PetShop.Service.Models.Products.OutputModels;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<AddProductInputServiceModel, Product>()
                .ForMember(x => x.ProductType, y => y.MapFrom(x => Enum.Parse(typeof(ProductType), x.ProductType)));
            this.CreateMap<Product, ListAllProductsByProductTypeServiceModel>();
            this.CreateMap<Product, ListAllProductsServiceModel>()
                .ForMember(x => x.ProductType, y=> y.MapFrom(x=> x.ProductType.ToString()));
            this.CreateMap<Product, ListAllProductsByNameServiceModel>()
                .ForMember(x => x.ProductType, y => y.MapFrom(x => x.ProductType.ToString()));
            this.CreateMap<EditProductInputServiceModel, Product>()
                .ForMember(x => x.ProductType, y => y.MapFrom(x => Enum.Parse(typeof(ProductType), x.ProductType)));
        }
    }
}
