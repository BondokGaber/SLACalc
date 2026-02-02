using SLACalc.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Infrastructure.Data.seeders
{
    public static class SlaConfigurationSeedData
    {
        public static SlaConfiguration[] GetSeedData()
        {
            return new[]
            {
                new SlaConfiguration("High", 4) { Id = 1 },
                new SlaConfiguration("Medium", 10) { Id = 2 },
                new SlaConfiguration("Low", 24) { Id = 3 }
            };
        }
    }
}
