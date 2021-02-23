namespace ProductShop
{
    using System.Linq;
    using AutoMapper;
    using ProductShop.Models;
    using ProductShop.DTO.Users;
    using ProductShop.DTO.Categories;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<Product, UsersSoldProductDTO>()
                .ForMember(x => x.BuyerFirstName, y => y.MapFrom(x => x.Buyer.FirstName))
                .ForMember(x => x.BuyerLastName, y => y.MapFrom(x => x.Buyer.LastName));

            this.CreateMap<User, UserWithSoldProductsDTO>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(x => x.ProductsSold.Where(p => p.Buyer != null)));

            this.CreateMap<Category, CategoriesByProductsDTO>()
                .ForMember(x => x.Category, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.ProductsCount, y => y.MapFrom(x => x.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(x => x.CategoryProducts.Average(cp => cp.Product.Price).ToString("f2")))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(x => x.CategoryProducts.Sum(cp => cp.Product.Price).ToString("f2")));
        }
    }
}
