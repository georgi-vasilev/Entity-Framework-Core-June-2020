namespace SalesDatabase.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using SalesDatabase.Data.IOManagment.Contracts;
    using SalesDatabase.Data.Models;
    using SalesDatabase.Data.Seeding.Contracts;

    public class ProductSeeder : ISeeder
    {
        private readonly Random random;
        private readonly SalesContext dbContext;
        private readonly IWriter writer;

        public ProductSeeder(SalesContext context, Random random, IWriter writer)
        {
            this.dbContext = context;
            this.random = random;
            this.writer = writer;
        }

        public void Seed()
        {
            ICollection<Product> products = new List<Product>();
            string[] names = new string[]
            {
                "CPU",
                "Motherboard",
                "GPU",
                "RAM",
                "SSD",
                "HDD",
                "CD-RAW",
                "Air Cooler",
                "Water Cooler",
                "Thermal paste"
            };

            for (int i = 0; i < 50; i++)
            {
                int nameIndex = this.random.Next(names.Length);
                string currentProductName = names[nameIndex];
                double quantity = this.random.Next(1000);
                decimal price = this.random.Next(5000) * 1.133m;

                Product product = new Product()
                {
                    Name = currentProductName,
                    Price = price,
                    Qunatity = quantity,
                };

                products.Add(product);

                this.writer.WriteLine($"Product (Name: {currentProductName} {quantity} {price}$ was added to the database.)");

            }

            this.dbContext
                 .Products
                 .AddRange(products);
            this.dbContext.SaveChanges();
        }
    }
}
