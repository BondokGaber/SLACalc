using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.entities
{
    public class BusinessHour : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public bool IsActive { get;  set; }

        private BusinessHour() { }

        public BusinessHour(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, bool isActive = true)
        {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            IsActive = isActive;

            Validate();
        }

        private void Validate()
        {
            if (StartTime >= EndTime)
                throw new ArgumentException("Start time must be before end time");

            if (StartTime < TimeSpan.Zero || EndTime > TimeSpan.FromHours(24))
                throw new ArgumentException("Invalid time range");
        }

        public void Update(TimeSpan startTime, TimeSpan endTime, bool isActive)
        {
            StartTime = startTime;
            EndTime = endTime;
            IsActive = isActive;
            Validate();
            UpdateModifiedDate();
        }

        public bool IsWithinWorkingHours(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek &&
                   dateTime.TimeOfDay >= StartTime &&
                   dateTime.TimeOfDay < EndTime;
        }
    }
}
