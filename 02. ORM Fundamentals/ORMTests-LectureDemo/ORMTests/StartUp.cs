using ORMTests.Models;
using System;
using System.Linq;

namespace ORMTests
{
    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new SoftuniContext();
            var employees = dbContext.Employees.Where(x => x.Department.Name == "Sales")
                .Select(x => new
                {
                    Name = x.FirstName + ' ' + x.LastName,
                    DepartmentName = x.Department.Name,
                    Manager = x.Manager.FirstName + ' ' + x.Manager.LastName
                });

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Name} => {employee.DepartmentName} => {employee.Manager}");
            }
        }
    }
}
