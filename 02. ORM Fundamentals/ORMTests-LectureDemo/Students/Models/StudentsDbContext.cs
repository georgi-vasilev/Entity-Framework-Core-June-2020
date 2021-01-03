namespace StudentsORMTest.Models
{
    using Microsoft.EntityFrameworkCore;

    public class StudentsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=./;Database=GradesDb;Integrated security=true;");
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Grade> Grades { get; set; }
    }
}
