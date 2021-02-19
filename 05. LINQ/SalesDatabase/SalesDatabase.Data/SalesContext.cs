﻿namespace SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using SalesDatabase.Data.Config;
    using SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }

        public SalesContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity
                    .Property(e => e.Name)
                    .IsUnicode();

                entity
                    .Property(e => e.Description)
                    .HasDefaultValue("No description");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                    .Property(e => e.Name)
                    .IsUnicode();

                entity
                    .Property(e => e.Email)
                    .IsUnicode(false);

                entity
                    .Property(e => e.CreditCardNumber)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity
                    .Property(e => e.Name)
                    .IsUnicode();
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity
                    .Property(e => e.Date)
                    .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
