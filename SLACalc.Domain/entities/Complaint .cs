using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.entities
{
    public class Complaint : BaseEntity
    {
        public string Priority { get; private set; }
        public DateTime CaptureDateTime { get; private set; }
        public DateTime ExpectedResolutionTime { get; private set; }
        public int SlaHours { get; private set; }
        public string? FilePath { get; private set; }
        public string? FileName { get; private set; }

        private Complaint() { }

        public Complaint(string priority, DateTime captureDateTime, int slaHours)
        {
            if (string.IsNullOrWhiteSpace(priority))
                throw new ArgumentException("Priority cannot be empty", nameof(priority));

            if (slaHours <= 0)
                throw new ArgumentException("SLA hours must be positive", nameof(slaHours));

            Priority = priority;
            CaptureDateTime = captureDateTime;
            SlaHours = slaHours;
        }

        public void CalculateResolutionTime(DateTime resolutionTime)
        {
            ExpectedResolutionTime = resolutionTime;
            UpdateModifiedDate();
        }

        public void AddFile(string fileName, string filePath)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty", nameof(fileName));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty", nameof(filePath));

            FileName = fileName;
            FilePath = filePath;
            UpdateModifiedDate();
        }
    }
}
