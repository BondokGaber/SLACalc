using SLACalc.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.Interfaces
{
    public interface IBusinessHourRepository : IRepository<BusinessHour>
    {
      
            Task<BusinessHour?> GetByDayOfWeekAsync(DayOfWeek dayOfWeek, CancellationToken cancellationToken = default);
            Task<IReadOnlyList<BusinessHour>> GetActiveBusinessHoursAsync(CancellationToken cancellationToken = default);
        
    }

}
