namespace RealEstates.ConsoleApplication
{
    using System;
    using System.Text;
    using RealEstates.Data;
    using RealEstates.Services;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var db = new RealEstateDbContext();
            IPropertiesService propertiesService = new PropertyService(db);
            IDistrictsService districtsService = new DistrictsService(db);

            //Console.Write("Min Price: ");
            //int minPrice = int.Parse(Console.ReadLine());
            //Console.Write("Max Price: ");
            //int maxPrice = int.Parse(Console.ReadLine());

            //var properties = propertiesService.SearchByPrice(minPrice, maxPrice);
            //foreach (var property in properties)
            //{
            //    Console.WriteLine($"{property.District}, fl. {property.Floor}, {property.Size}m², {property.Year}, {property.Price}€, {property.PropertyType}, {property.BuildingType}");
            //}

            var districts = districtsService.GetTopDistrictsByAveragePrice(10);

            foreach (var district in districts)
            {
                Console.WriteLine($"{district.District} => Price: {district.AveragePrice:0.00} ({district.MinPrice} - {district.MaxPrice}) => {district.PropertiesCount} properties");
            }
        }
    }
}
