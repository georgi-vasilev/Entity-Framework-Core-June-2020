namespace HospitalSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using HospitalSystem.Data.Config;
    using HospitalSystem.Data.Models;

    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {

        }
        public HospitalDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients;

        public DbSet<Medicament> Medicaments;

        public DbSet<Visitation> Visitations;

        public DbSet<Diagnose> Diagnoses;

        public DbSet<PatientMedicament> PatientMedicaments;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity => 
            {
                entity
                    .Property(e => e.Email)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(pm => new { pm.PatientId, pm.MedicamentId});
            });
        }
    }
}
