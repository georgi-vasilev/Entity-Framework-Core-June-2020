namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using CarDealer.Data;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    using CarDealer.XMLHelper;
    using CarDealer.Dtos.Export;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext db = new CarDealerContext();
            //ResetDatabse(db);

            var result = GetTotalSalesByCustomer(db);
            string path = "../../../Results/customers-total-sales.xml";

            File.WriteAllText(path, result);
        }
        private static void ResetDatabse(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database successfully deleted.");
            db.Database.EnsureCreated();
            Console.WriteLine("Database successfully created.");
        }

        //Problem 01
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            const string rootElement = "Suppliers";

            var suppliersDtos = XmlConverter.Deserializer<ImportSupplierDto>(inputXml, rootElement);

            var suppliers = suppliersDtos
                .Select(s => new Supplier
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        //Problem 02
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            const string rootElement = "Parts";

            var supliersIds = context.Suppliers.Select(x => x.Id).ToHashSet();
            var partsDtos = XmlConverter.Deserializer<ImportPartDto>(inputXml, rootElement);


            var parts = partsDtos.Where(x => supliersIds.Contains(x.SupplierId))
                .Select(p => new Part
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = p.SupplierId
                })
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        //Problem 03
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            const string rootElement = "Cars";

            var carsDtos = XmlConverter.Deserializer<ImportCarsDto>(inputXml, rootElement);

            List<Car> cars = new List<Car>();

            foreach (var carDto in carsDtos)
            {
                var uniqueParts = carDto.Parts
                    .Select(p => p.Id)
                    .Distinct()
                    .ToArray();
                var realParts = uniqueParts
                    .Where(id => context.Parts.Any(i => i.Id == id));

                var car = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance,
                    PartCars = realParts.Select(id => new PartCar()
                    {
                        PartId = id
                    })
                    .ToArray()
                };

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        //Problem 04
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            const string rootElement = "Customers";

            var customerDtos = XmlConverter.Deserializer<ImportCustomerDto>(inputXml, rootElement);


            var customers = customerDtos
                .Select(c => new Customer
                {
                    Name = c.Name,
                    BirthDate = DateTime.Parse(c.BirthDate),
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //Problem 05
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            const string rootElement = "Sales";

            var carIds = context.Cars.Select(c => c.Id).ToHashSet();
            var salesDto = XmlConverter.Deserializer<ImportSalesDto>(inputXml, rootElement);

            var sales = salesDto.Where(x => carIds.Contains(x.CarId))
                .Select(s => new Sale
                {
                    CarId = s.CarId,
                    CustomerId = s.CustomerId,
                    Discount = s.Discount
                })
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        //Problem 06
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            const string rootElement = "cars";

            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .Select(c => new ExportCarDistanceDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToList();

            var xmlResult = XmlConverter.Serialize(cars, rootElement);

            return xmlResult;

        }

        //Problem 07
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            const string rootElement = "cars";

            var cars = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new ExportCarMakeBmwDto
                {
                    CarId = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            var xmlResult = XmlConverter.Serialize(cars, rootElement);

            return xmlResult;
        }

        //Problem 08
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            const string rootElement = "suppliers";

            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new ExportLocalSupplierDto
                {
                    SupplierId = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var xmlResult = XmlConverter.Serialize(suppliers, rootElement);

            return xmlResult;
        }

        //Problem 09
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            const string rootElement = "cars";

            var cars = context.Cars
                .Select(c => new ExportCarPartsDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(p => new ExportPartsDto
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToArray()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToList();

            var xmlResult = XmlConverter.Serialize(cars, rootElement);

            return xmlResult;
        }

        //Problem 10
        //TODO: MoneySpent throws an error
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            const string rootElement = "customers";

            //doesnt work for netcore 3.1 (nested aggregations)
            //var customers = context
            //    .Customers
            //    .Where(c => c.Sales.Count >= 1)
            //    .Select(c => new ExportCustomersTotalSalesDto
            //    {
            //        FullName = c.Name,
            //        BoughtCars = c.Sales.Count,
            //        MoneySpent = 
            //    })
            //    .OrderByDescending(c => c.MoneySpent)
            //    .ThenByDescending(c => c.BoughtCars)
            //    .ToList();

            //var sales = context.Customers
            //    .Where(c => c.Sales.Count >= 1)
            //    .Select(c => new
            //    {
            //        Fullname = c.Name,
            //        BoughtCars = c.Sales
            //            .Select(c => new
            //            {
            //                PartsPrice = c.Car.PartCars.Sum(p => p.Part.Price)
            //            })
            //    })
            //    .AsEnumerable()
            //    .Select(c => new ExportCustomersTotalSalesDto
            //    {
            //        FullName = c.Fullname,
            //        BoughtCars = c.BoughtCars.Count(),
            //        MoneySpent = c.BoughtCars.Sum(p => p.PartsPrice)
            //    })
            //    .OrderByDescending(c => c.MoneySpent)
            //    .ThenByDescending(c => c.BoughtCars)
            //    .ToList();


            //Eager loading.
            //var sales = context.Customers
            //    .Include(c=> c.Sales)
            //    .ThenInclude(s => s.Car.PartCars)
            //    .ThenInclude(p => p.Part)
            //    .Where(c => c.Sales.Count >= 1)
            //    .AsEnumerable()
            //    .GroupBy(c => new
            //    {
            //        c.Id,
            //        c.Name,
            //    })
            //    .Select(c => new ExportCustomersTotalSalesDto
            //    {
            //        FullName = c.Key.Name,
            //        BoughtCars = c.Count(),
            //        MoneySpent = c.Sum(cl => cl.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price)))
            //    })
            //    .OrderByDescending(c => c.MoneySpent)
            //    .ThenBy(c => c.BoughtCars)
            //    .ToList();

            var cars = context.Cars
                .Select(c => new
                {
                    carId = c.Id,
                    PartsPrice = c.PartCars.Sum(p => p.Part.Price)
                })
                .ToDictionary(x => x.carId, y => y.PartsPrice);

            var sales = context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(c => new
                {
                    c.Name,
                    carIds = c.Sales.Select(s => s.Car.Id).ToList()
                })
                .ToList()
                .Select(c => new ExportCustomersTotalSalesDto
                {
                    FullName = c.Name,
                    BoughtCars = c.carIds.Count,
                    MoneySpent = c.carIds.Sum(carId => cars[carId])
                })
                .OrderByDescending(c => c.MoneySpent)
                .ThenBy(c => c.BoughtCars)
                .ToList();




            var xmlResult = XmlConverter.Serialize(sales, rootElement);
            return xmlResult;
        }

        //Problem 11
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            const string rootElement = "sales";

            var result = context.Sales
                .Select(s => new ExportSaleDto
                {
                    Car = new ExportCarDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(p => p.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(p => p.Part.Price) - (s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100),
                })
                .ToList();

            var xmlResult = XmlConverter.Serialize(result, rootElement);

            return xmlResult;
        }
    }
}