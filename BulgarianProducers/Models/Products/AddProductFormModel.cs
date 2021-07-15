using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianProducers.Data.DataConstants;
namespace BulgarianProducers.Models.Products
{
    public class AddProductFormModel
    {
        [Required]
        [MaxLength(ProductNameMaxLenght)]
        [MinLength(3, ErrorMessage = "Името на продукта трябва да е поне 3 символа!")]
        public string Name { get; set; }
        [Required]
        [MaxLength(ProductDescriptionMaxLenght)]
        [MinLength(10, ErrorMessage = "Дължината на описанието трябва да е поне 10 символа!")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        //TODO: Files to be on the file system.
        [Required]
        [Url]
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<ProductsCategoryModel> Categories { get; set; }
    }
}
