namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    using ProductShop.XMLHelper;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            ProductShopContext db = new ProductShopContext();
            //ResetDatabse(db);

            var prodcutsInRage = GetUsersWithProducts(db);
            File.WriteAllText("../../../Results/users-and-products.xml", prodcutsInRage);
        }
        private static void ResetDatabse(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database successfully deleted.");
            db.Database.EnsureCreated();
            Console.WriteLine("Database successfully created.");
        }
        //Problem 01
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            const string rootElement = "Users";

            var usersResult = XmlConverter.Deserializer<ImportUserDto>(inputXml, rootElement);

            var users = usersResult
                .Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //Problem 02
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            const string rootElement = "Products";

            var productDtos = XmlConverter.Deserializer<ImportProductDto>(inputXml, rootElement);

            var products = productDtos.Select(p => new Product
            {
                Name = p.Name,
                Price = p.Price,
                SellerId = p.SellerId,
                BuyerId = p.BuyerId
            })
              .ToList();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        //Problem 03
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            const string rootElement = "Categories";

            var categoriesDto = XmlConverter.Deserializer<ImportCategoryDto>(inputXml, rootElement);

            var categories = categoriesDto
                .Where(c => c.Name != null)
                .Select(c => new Category
                {
                    Name = c.Name
                })
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //Problem 04
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            const string rootElement = "CategoryProducts";

            var categoryProductsDtos = XmlConverter.Deserializer<ImportCategoryProductDto>(inputXml, rootElement);

            var categoryProducts = categoryProductsDtos
                .Where(i => context.Categories.Any(s => s.Id == i.CategoryId) && context.Products.Any(s => s.Id == i.ProductId))
                .Select(c => new CategoryProduct
                {
                    CategoryId = c.CategoryId,
                    ProductId = c.ProductId
                })
                .ToList();



            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //Problem 05
        public static string GetProductsInRange(ProductShopContext context)
        {
            const string rootElement = "Products";

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(x => new ExportProductInfoDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();

            var result = XmlConverter.Serialize(products, rootElement);

            return result;
        }

        //Problem 06
        public static string GetSoldProducts(ProductShopContext context)
        {
            const string rootElement = "Users";

            var usersWithProducts = context.Users
                .Where(x => x.ProductsSold.Any())
                .Select(x => new ExportUserSoldProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold
                    .Select(p => new UserProductDto
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToArray()
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToList();

            var result = XmlConverter.Serialize(usersWithProducts, rootElement);

            return result;
        }

        //Problem 07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            const string rootElement = "Users";

            var categories = context.Categories
                .Select(c => new ExportCategoryDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(p => p.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();

            var result = XmlConverter.Serialize(categories, rootElement);

            return result;
        }

        //Problem 08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            const string rootElement = "Users";

            var usersAndProducts = context.Users
                .Where(p => p.ProductsSold.Any())
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProduct = new ExportProductCountDto
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold.Select(p => new ExportProductDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                    }
                })
                .OrderByDescending(x => x.SoldProduct.Count)
                .Take(10)
                .ToArray();

            var resultDto = new ExportUserCountDto
            {
                Count = context.Users.Count(u => u.ProductsSold.Any()),
                Users = usersAndProducts
            };

            var xmlResult = XmlConverter.Serialize(resultDto, rootElement);

            return xmlResult;
        }
    }
}