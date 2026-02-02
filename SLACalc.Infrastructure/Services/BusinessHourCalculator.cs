using SLACalc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Infrastructure.Services
{
    public class BusinessHourCalculator : IBusinessHourCalculator
    {
        private readonly IBusinessHourRepository _businessHourRepository;
        private readonly IBusinessClosureRepository _businessClosureRepository;

        public BusinessHourCalculator(
            IBusinessHourRepository businessHourRepository,
            IBusinessClosureRepository businessClosureRepository)
        {
            _businessHourRepository = businessHourRepository;
            _businessClosureRepository = businessClosureRepository;
        }

        public DateTime CalculateDeadline(DateTime startDateTime, int slaHours)
        {
            DateTime slaExpiration = startDateTime.AddHours(slaHours);

            // If starting outside business hours, move to next business hour
            if (!IsBusinessHour(startDateTime))
            {
                DateTime nextBusinessHour = GetNextBusinessHour(startDateTime);
                slaExpiration = nextBusinessHour.AddHours(slaHours);
            }

            // Adjust for non-business hours in the SLA period
            DateTime currentHour = startDateTime.AddHours(1).Date.AddHours(startDateTime.AddHours(1).Hour);
            int remainingBusinessHours = slaHours;

            while (remainingBusinessHours > 0)
            {
                if (!IsBusinessHour(currentHour))
                {
                    // Extend deadline for non-business hour
                    slaExpiration = slaExpiration.AddHours(1);
                }
                else
                {
                    remainingBusinessHours--;
                }
                currentHour = currentHour.AddHours(1);
            }

            return slaExpiration;
        }

        public bool IsBusinessHour(DateTime dateTime)
        {
            // Check if it's a business day
            if (!IsBusinessDay(dateTime.Date))
                return false;

            // Check working hours for that day
            var businessHour = _businessHourRepository.GetByDayOfWeekAsync(dateTime.DayOfWeek).Result;
            if (businessHour == null || !businessHour.IsActive)
                return false;

            var timeOfDay = dateTime.TimeOfDay;
            return timeOfDay >= businessHour.StartTime && timeOfDay < businessHour.EndTime;
        }

        public bool IsBusinessDay(DateTime date)
        {
            // Check if day has working hours configured
            var businessHour = _businessHourRepository.GetByDayOfWeekAsync(date.DayOfWeek).Result;
            if (businessHour == null || !businessHour.IsActive)
                return false;

            // Check for full-day closures
            var isDateClosed = _businessClosureRepository.IsDateClosedAsync(date).Result;
            return !isDateClosed;
        }

        public DateTime GetNextBusinessHour(DateTime fromDateTime)
        {
            DateTime current = fromDateTime.AddHours(1);
            int maxIterations = 1000; // Safety limit

            for (int i = 0; i < maxIterations; i++)
            {
                if (IsBusinessHour(current))
                    return current;

                current = current.AddHours(1);

                // If we've crossed to a new day, check if it's a business day
                if (current.Date != fromDateTime.Date && !IsBusinessDay(current.Date))
                {
                    // Skip to next business day at start of working hours
                    current = GetNextBusinessDayStart(current);
                }
            }

            throw new InvalidOperationException("Could not find next business hour within reasonable iterations");
        }

        private DateTime GetNextBusinessDayStart(DateTime fromDateTime)
        {
            DateTime nextDay = fromDateTime.Date.AddDays(1);
            int maxIterations = 365; // Safety limit for 1 year

            for (int i = 0; i < maxIterations; i++)
            {
                if (IsBusinessDay(nextDay))
                {
                    var businessHour = _businessHourRepository.GetByDayOfWeekAsync(nextDay.DayOfWeek).Result;
                    return nextDay.Date.Add(businessHour!.StartTime);
                }

                nextDay = nextDay.AddDays(1);
            }

            throw new InvalidOperationException("Could not find next business day within 1 year");
        }
    }
}
