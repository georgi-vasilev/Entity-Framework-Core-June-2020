using StudentsORMTest.Models;

namespace Students
{
    public class StartUp
    {
        public static void Main()
        {
            var dbContext = new StudentsDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Courses.Add(new Course { Name = "Entity Framework Core" });
            dbContext.SaveChanges();
        }
    }
}
