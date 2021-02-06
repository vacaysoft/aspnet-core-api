using System;

namespace VacaySoft.Domain.Entities
{
    public abstract class AuditableEntity<T> : BaseEntity<T>
    {
        public DateTime CreatedDateTime { get; set; }
        public Guid CreatedByUserProfile { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public Guid UpdatedByUserProfile { get; set; }
    }
}
