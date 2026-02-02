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
    public class SlaConfigurationRepository : BaseRepository<SlaConfiguration>, ISlaConfigurationRepository
    {
        public SlaConfigurationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SlaConfiguration?> GetByPriorityAsync(string priority, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.Priority == priority && s.IsActive, cancellationToken);
        }

        public async Task<IReadOnlyList<string>> GetAllPrioritiesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(s => s.IsActive)
                .Select(s => s.Priority)
                .OrderBy(p => p)
                .ToListAsync(cancellationToken);
        }
    }
}
