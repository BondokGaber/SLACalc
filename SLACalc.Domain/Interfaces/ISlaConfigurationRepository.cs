using SLACalc.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.Interfaces
{
    public interface ISlaConfigurationRepository : IRepository<SlaConfiguration>
    {
        Task<SlaConfiguration?> GetByPriorityAsync(string priority, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<string>> GetAllPrioritiesAsync(CancellationToken cancellationToken = default);
    }
}
