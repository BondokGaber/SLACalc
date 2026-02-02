using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.Interfaces
{
    public interface IBusinessHourCalculator
    {
        DateTime CalculateDeadline(DateTime startDateTime, int slaHours);
        bool IsBusinessHour(DateTime dateTime);
        bool IsBusinessDay(DateTime date);
        DateTime GetNextBusinessHour(DateTime fromDateTime);
    }

}
