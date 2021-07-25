using System;

namespace BulgarianProducers.Infrastructure
{
    public abstract class BaseEntity
    {
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
