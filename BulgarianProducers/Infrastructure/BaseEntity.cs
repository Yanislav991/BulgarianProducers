using BulgarianProducers.Data.Models;
using System;

namespace BulgarianProducers.Infrastructure
{
    public abstract class BaseEntity
    {
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
