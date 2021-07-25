using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BulgarianProducers.Data.DataConstants;

namespace BulgarianProducers.Data.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLenght)]
        public string Name { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
