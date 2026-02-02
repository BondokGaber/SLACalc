using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Domain.entities
{
    public abstract class BaseEntity
    {
        public int Id { get;  set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        // Add this method
        protected void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public void UpdateModifiedDate()
        {
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
