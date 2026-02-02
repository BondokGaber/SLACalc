using SLACalc.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Infrastructure.Data.seeders
{
    public static class BusinessHourSeedData
    {
        public static BusinessHour[] GetSeedData()
        {
            return new[]
            {
                new BusinessHour(DayOfWeek.Monday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 1 },
                new BusinessHour(DayOfWeek.Tuesday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 2 },
                new BusinessHour(DayOfWeek.Wednesday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 3 },
                new BusinessHour(DayOfWeek.Thursday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 4 },
                new BusinessHour(DayOfWeek.Friday, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0)) { Id = 5 },
                // Saturday and Sunday are not included by default (weekend)
            };
        }
    }

}
