using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.entities
{
    public class SlaConfiguration : BaseEntity
    {
        public string Priority { get; private set; }
        public int SlaHours { get; private set; }
        public bool IsActive { get;  set; }

        private SlaConfiguration() { }

        public SlaConfiguration(string priority, int slaHours, bool isActive = true)
        {
            if (string.IsNullOrWhiteSpace(priority))
                throw new ArgumentException("Priority cannot be empty", nameof(priority));

            if (slaHours <= 0)
                throw new ArgumentException("SLA hours must be positive", nameof(slaHours));

            Priority = priority;
            SlaHours = slaHours;
            IsActive = isActive;
        }

        public void Update(int slaHours, bool isActive)
        {
            if (slaHours <= 0)
                throw new ArgumentException("SLA hours must be positive", nameof(slaHours));

            SlaHours = slaHours;
            IsActive = isActive;
            UpdateModifiedDate();
        }
    }
}
