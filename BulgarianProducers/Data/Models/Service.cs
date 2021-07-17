using BulgarianProducers.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianProducers.Data.DataConstants;

namespace BulgarianProducers.Data.Models
{
    public class Service : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(ProductNameMaxLenght)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public TimeSpan? TimeNeeded { get; set; }
        [Required]
        [MaxLength(250)]
        public string AdditionalInformation { get; set; }
        public string ImageUrl { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
