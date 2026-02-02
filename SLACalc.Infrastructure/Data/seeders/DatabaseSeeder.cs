using SLACalc.Domain.entities;
using SLACalc.Infrastructure.Data;

namespace SlaCalculation.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.BusinessHours.Any())
            {
                await SeedBusinessHours(context);
            }

            if (!context.SlaConfigurations.Any())
            {
                await SeedSlaConfigurations(context);
            }

            if (!context.BusinessClosures.Any())
            {
                await SeedBusinessClosures(context);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedBusinessHours(ApplicationDbContext context)
        {
            var businessHours = new[]
            {
                   new BusinessHour(DayOfWeek.Monday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 1 },
                new BusinessHour(DayOfWeek.Tuesday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 2 },
                new BusinessHour(DayOfWeek.Wednesday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 3 },
                new BusinessHour(DayOfWeek.Thursday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 4 },
                new BusinessHour(DayOfWeek.Friday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 5 },
                // Saturday and Sunday are not included by default (weekend)
            };

            await context.BusinessHours.AddRangeAsync(businessHours);
        }

        private static async Task SeedSlaConfigurations(ApplicationDbContext context)
        {
            var slaConfigs = new[]
            {
                new SlaConfiguration("High", 4),
                new SlaConfiguration("Medium", 10),
                new SlaConfiguration("Low", 24),
      
            };

            await context.SlaConfigurations.AddRangeAsync(slaConfigs);
        }

        private static async Task SeedBusinessClosures(ApplicationDbContext context)
        {
            var currentYear = DateTime.UtcNow.Year;

            var closures = new[]
            {
                new BusinessClosure(new DateTime(currentYear, 1, 1), "New Year's Day", null, null, true),
                
                new BusinessClosure(new DateTime(currentYear, 6, 15), "Annual Company Meeting",
                    new TimeSpan(14, 0, 0), new TimeSpan(16, 0, 0), false)
            };

            await context.BusinessClosures.AddRangeAsync(closures);
        }

        private static DateTime GetThanksgivingDate(int year)
        {
            var november = new DateTime(year, 11, 1);
            var daysUntilThursday = (DayOfWeek.Thursday - november.DayOfWeek + 7) % 7;
            return november.AddDays(daysUntilThursday + 21);
        }
    }
}