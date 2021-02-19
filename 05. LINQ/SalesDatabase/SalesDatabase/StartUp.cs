namespace SalesDatabase
{
    using System;
    using System.Collections.Generic;
    using SalesDatabase.Data;
    using SalesDatabase.Data.IOManagment;
    using SalesDatabase.Data.IOManagment.Contracts;
    using SalesDatabase.Data.Seeding;
    using SalesDatabase.Data.Seeding.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            //SalesContext dbContext = new SalesContext();
            //Random random = new Random();
            //IWriter consoleWriter = new ConsoleWriter();

            //ICollection<ISeeder> seeders = new List<ISeeder>();
            //seeders.Add(new ProductSeeder(dbContext, random, consoleWriter));
            //seeders.Add(new StoreSeeder(dbContext, consoleWriter));

            //foreach (ISeeder seeder in seeders)
            //{
            //    seeder.Seed();
            //}
        }
    }
}
