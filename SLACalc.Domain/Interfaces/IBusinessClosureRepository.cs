using SLACalc.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.Interfaces
{
    public interface IBusinessClosureRepository : IRepository<BusinessClosure>
    {
        Task<IReadOnlyList<BusinessClosure>> GetClosuresForDateAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<bool> IsDateClosedAsync(DateTime date, CancellationToken cancellationToken = default);
    }
}
