namespace PetShop.Services
{
    using System;
    using System.Linq;

    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using PetShop.Data;
    using PetShop.Models;
    using PetShop.Common;
    using PetShop.Models.Enumerations;
    using PetShop.Service.Models.Products.InputModels;
    using PetShop.Service.Models.Products.OutputModels;

    public class ProductService : IProductService
    {
        private readonly PetShopDbContext db;
        private readonly IMapper mapper;

        public ProductService(PetShopDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public void AddProduct(AddProductInputServiceModel model)
        {
            try
            {
                Product product = this.mapper.Map<Product>(model);
                this.db.Products.Add(product);
                this.db.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentException(ExceptionMessages.InvalidProductType);
            }

        }

        public ICollection<ListAllProductsServiceModel> GetAll()
        {
            var products = this.db
                .Products
                .ProjectTo<ListAllProductsServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return products;
        }

        public ICollection<ListAllProductsByNameServiceModel> SerachByName(string name, bool caseSensitive)
        {
            ICollection<ListAllProductsByNameServiceModel> products;

            if (caseSensitive)
            {
                products = this.db.Products
                    .Where(p => p.Name.Contains(name))
                    .ProjectTo<ListAllProductsByNameServiceModel>(this.mapper.ConfigurationProvider)
                    .ToList();
            }
            else
            {
                products = this.db.Products
                   .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                   .ProjectTo<ListAllProductsByNameServiceModel>(this.mapper.ConfigurationProvider)
                   .ToList();
            }

            return products;
        }

        public ICollection<ListAllProductsByProductTypeServiceModel> ListAllByProductType(string type)
        {
            ProductType productType;

            bool hasParsed = Enum.TryParse<ProductType>(type, true, out productType);

            if (!hasParsed)
            {
                throw new ArgumentException(ExceptionMessages.InvalidProductType);
            }

            var productsServiceModels = this.db.Products
                .Where(p => p.ProductType == productType)
                .ProjectTo<ListAllProductsByProductTypeServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return productsServiceModels;
        }

        public bool RemoveById(string id)
        {
            Product productToRemove = this.db
                .Products
                .Find(id);

            if (productToRemove == null)
            {
                throw new ArgumentException(ExceptionMessages.ProductNotFound);
            }

            this.db.Products.Remove(productToRemove);
            int rowsAffected = this.db.SaveChanges();

            bool wasDeleted = rowsAffected == 1;

            return wasDeleted;
        }

        public bool RemoveByName(string name)
        {
            Product productToRemove = this.db
               .Products
               .FirstOrDefault(p => p.Name == name);

            if (productToRemove == null)
            {
                throw new ArgumentException(ExceptionMessages.ProductNotFound);
            }

            this.db.Products.Remove(productToRemove);
            int rowsAffected = this.db.SaveChanges();

            bool wasDeleted = rowsAffected == 1;

            return wasDeleted;
        }

        public void EditProduct(string id, EditProductInputServiceModel model)
        {
            try
            {
                Product product = this.mapper.Map<Product>(model);

                Product productToUpdate = this.db.Products
                    .Find(id);

                if (productToUpdate == null)
                {
                    throw new ArgumentException(ExceptionMessages.ProductNotFound);
                }

                productToUpdate.Name = product.Name;
                productToUpdate.ProductType = product.ProductType;
                productToUpdate.Price = product.Price;

                this.db.SaveChanges();
            }
            catch(ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception)
            {
                throw new ArgumentException(ExceptionMessages.InvalidProductType);
            }
        }
    }
}
