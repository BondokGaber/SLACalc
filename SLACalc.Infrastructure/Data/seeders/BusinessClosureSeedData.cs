using SLACalc.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Infrastructure.Data.seeders
{
    public static class BusinessClosureSeedData
    {
        public static BusinessClosure[] GetSeedData()
        {
            var currentYear = DateTime.UtcNow.Year;

            return new[]
            {
                // New Year's Day (January 1st) - Full day, recurring
                new BusinessClosure(
                    new DateTime(currentYear, 1, 1),
                    "New Year's Day",
                    null,
                    null,
                    true) { Id = 1 },
                
                // Christmas Day (December 25th) - Full day, recurring
                new BusinessClosure(
                    new DateTime(currentYear, 12, 25),
                    "Eid-Adha Day",
                    null,
                    null,
                    true) { Id = 2 },
                
                // Example: Company Event (Partial day closure)
                new BusinessClosure(
                    new DateTime(currentYear, 6, 15),
                    "Annual Company Meeting",
                    new TimeSpan(14, 0, 0),
                    new TimeSpan(16, 0, 0),
                    false) { Id = 3 }
            };
        }
    }
}
