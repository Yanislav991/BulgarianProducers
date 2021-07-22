using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Models
{
    public class ProductsAndServicesListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsProduct { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
