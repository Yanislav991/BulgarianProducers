using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BulgarianProducers.Data.DataConstants;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulgarianProducers.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(ProductNameMaxLenght)]
        public string Name { get; set; }
        [Required]
        [MaxLength(ProductDescriptionMaxLenght)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        //TODO: Files to be on the file system.
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
