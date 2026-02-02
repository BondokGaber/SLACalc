using Microsoft.EntityFrameworkCore;
using SLACalc.Domain.entities;
using SLACalc.Infrastructure.Data.seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BusinessHour> BusinessHours => Set<BusinessHour>();
        public DbSet<BusinessClosure> BusinessClosures => Set<BusinessClosure>();
        public DbSet<SlaConfiguration> SlaConfigurations => Set<SlaConfiguration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entities
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Apply seed data
            modelBuilder.ApplySeedData();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;
                entity.UpdateModifiedDate();

                if (entityEntry.State == EntityState.Added)
                {
                    // Use reflection to set protected property
                    var property = entity.GetType().GetProperty("CreatedAt");
                    property?.SetValue(entity, DateTime.UtcNow);
                }
            }
        }
    }
}
