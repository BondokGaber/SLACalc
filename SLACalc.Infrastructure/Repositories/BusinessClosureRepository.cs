using Microsoft.EntityFrameworkCore;
using SLACalc.Domain.entities;
using SLACalc.Domain.Interfaces;
using SLACalc.Infrastructure.Data;


namespace SLACalc.Infrastructure.Repositories
{
    public class BusinessClosureRepository : BaseRepository<BusinessClosure>, IBusinessClosureRepository
    {
        public BusinessClosureRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<BusinessClosure>> GetClosuresForDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            var day = date.Day;
            var month = date.Month;

            return await _dbSet
                .Where(c =>
                    // Exact date match for non-recurring closures
                    (!c.IsRecurring && c.ClosureDate.Date == date.Date) ||
                    // Day and month match for recurring closures
                    (c.IsRecurring && c.ClosureDate.Day == day && c.ClosureDate.Month == month))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsDateClosedAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            var closures = await GetClosuresForDateAsync(date, cancellationToken);
            return closures.Any(c => c.IsFullDayClosure);
        }
    }
}
