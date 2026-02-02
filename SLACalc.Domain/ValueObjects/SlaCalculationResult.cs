using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.ValueObjects
{
    public class SlaCalculationResult : IEquatable<SlaCalculationResult>
    {
        public DateTime CaptureDateTime { get; }
        public string Priority { get; }
        public int SlaHours { get; }
        public DateTime ExpectedResolutionTime { get; }
        public TimeSpan TotalProcessingTime { get; }

        public SlaCalculationResult(DateTime captureDateTime, string priority,
            int slaHours, DateTime expectedResolutionTime)
        {
            CaptureDateTime = captureDateTime;
            Priority = priority;
            SlaHours = slaHours;
            ExpectedResolutionTime = expectedResolutionTime;
            TotalProcessingTime = expectedResolutionTime - captureDateTime;
        }

        public bool Equals(SlaCalculationResult? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return CaptureDateTime == other.CaptureDateTime &&
                   Priority == other.Priority &&
                   SlaHours == other.SlaHours &&
                   ExpectedResolutionTime == other.ExpectedResolutionTime;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as SlaCalculationResult);
        }

        //use for combiation  collections like Dictionary
        public override int GetHashCode()
        {
            return HashCode.Combine(CaptureDateTime, Priority, SlaHours, ExpectedResolutionTime);
        }
    }
}
