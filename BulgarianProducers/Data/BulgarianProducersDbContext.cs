using BulgarianProducers.Data.Models;
using BulgarianProducers.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulgarianProducers.Data
{
    public class BulgarianProducersDbContext : IdentityDbContext
    {
        public BulgarianProducersDbContext(DbContextOptions<BulgarianProducersDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<AgriculturalEvent> AgriculturalEvents { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Service>()
              .HasOne(s => s.ServiceType)
              .WithMany(st => st.Services)
              .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
