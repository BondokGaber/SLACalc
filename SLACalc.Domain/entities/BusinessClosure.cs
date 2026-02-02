using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.entities
{
    public class BusinessClosure : BaseEntity
    {
        public DateTime ClosureDate { get; private set; }
        public TimeSpan? StartTime { get; private set; }
        public TimeSpan? EndTime { get; private set; }
        public string Description { get; private set; }
        public bool IsRecurring { get; private set; }
        public bool IsFullDayClosure => !StartTime.HasValue && !EndTime.HasValue;

        private BusinessClosure() { }

        public BusinessClosure(DateTime closureDate, string description,
            TimeSpan? startTime = null, TimeSpan? endTime = null, bool isRecurring = false)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty", nameof(description));

            ClosureDate = closureDate.Date;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            IsRecurring = isRecurring;

            Validate();
        }

        private void Validate()
        {
            if (StartTime.HasValue != EndTime.HasValue)
                throw new ArgumentException("Both start and end times must be provided or both must be null");

            if (StartTime.HasValue && EndTime.HasValue)
            {
                if (StartTime.Value >= EndTime.Value)
                    throw new ArgumentException("Start time must be before end time");
            }
        }

        public bool IsClosureActive(DateTime dateTime)
        {
            var date = dateTime.Date;
            var time = dateTime.TimeOfDay;

            // Check if this is a recurring closure
            if (IsRecurring)
            {
                if (ClosureDate.Month != date.Month || ClosureDate.Day != date.Day)
                    return false;
            }
            else
            {
                if (ClosureDate != date)
                    return false;
            }

            // Full day closure
            if (IsFullDayClosure)
                return true;

            // Partial day closure
            return time >= StartTime!.Value && time < EndTime!.Value;
        }
    }
}
