using BulgarianProducers.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianProducers.Data.DataConstants;

namespace BulgarianProducers.Models.Services
{
    public class AddServiceFormModel
    {
        [Required]
        [MaxLength(ProductNameMaxLenght)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Price { get; set; }
        //часове
        public int? TimeNeeded { get; set; }
        [Required]
        [MaxLength(250)]
        public string AdditionalInformation { get; set; }
        [Required]
        [Url]
        public string ImageUrl { get; set; }
        public int ServiceTypeId { get; set; }
        public IEnumerable<ServiceTypeModel> ServiceTypes { get; set; }
    }
}
