namespace StudentSystem.Data
{
    using StudentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options) 
            : base (options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);

                entity
                    .Property(s => s.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .IsRequired();

                entity
                    .Property(s => s.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired(false);

                entity
                    .Property(s => s.RegisteredOn)
                    .ValueGeneratedOnAdd();

                entity
                    .Property(s => s.Birthday)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);

                entity
                    .Property(c => c.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity
                    .Property(c => c.Description)
                    .IsUnicode(true)
                    .IsRequired(false);

                entity
                    .Property(c => c.StartDate)
                    .IsRequired(true);

                entity
                    .Property(c => c.EndDate)
                    .IsRequired(true);

                entity
                    .Property(c => c.Price)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Resource>(entity => 
            {
                entity.HasKey(r => r.ResourceId);

                entity
                    .Property(r => r.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity
                    .Property(r => r.Url)
                    .IsUnicode(false)
                    .IsRequired(true);

                entity
                    .HasOne(r => r.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(r => r.CourseId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(h => h.HomeworkId);

                entity
                    .Property(h => h.SubmissionTime)
                    .ValueGeneratedOnAdd();

                entity
                    .HasOne(h => h.Student)
                    .WithMany(s => s.Homeworks)
                    .HasForeignKey(h => h.StudentId);

                entity
                    .HasOne(h => h.Course)
                    .WithMany(c => c.Homeworks)
                    .HasForeignKey(h => h.CourseId);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                entity
                    .HasOne(sc => sc.Student)
                    .WithMany(s => s.Courses);

                entity
                    .HasOne(sc => sc.Course)
                    .WithMany(c => c.Students);
            });
        }
    }
}
