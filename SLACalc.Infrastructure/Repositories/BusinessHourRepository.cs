using Microsoft.EntityFrameworkCore;
using SLACalc.Domain.entities;
using SLACalc.Domain.Interfaces;
using SLACalc.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Infrastructure.Repositories
{
    public class BusinessHourRepository : BaseRepository<BusinessHour>, IBusinessHourRepository
    {
        public BusinessHourRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<BusinessHour?> GetByDayOfWeekAsync(DayOfWeek dayOfWeek, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(b => b.DayOfWeek == dayOfWeek && b.IsActive, cancellationToken);
        }

        public async Task<IReadOnlyList<BusinessHour>> GetActiveBusinessHoursAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(b => b.IsActive)
                .OrderBy(b => b.DayOfWeek)
                .ToListAsync(cancellationToken);
        }
    }
}
