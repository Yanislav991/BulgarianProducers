
using BulgarianProducers.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BulgarianProducers.Services.Models
{
    public class ProductsAndServicesQueryModel
    {
        [Display(Name = "Търси по име")]
        public string SearchTerm { get; set; }
        public ProductsAndServicesSorting Sorting { get; set; }
        [Display(Name = "Покажи продукти")]
        public bool ShowProducts { get; set; }
        [Display(Name = "Покажи услуги")]
        public bool ShowServices { get; set; }
        public int CurrentPage { get; set; }
        public int TotalElementsCount { get; set; }
        public List<ProductsAndServicesListingModel> ProductsAndServices { get; set; }
    }
}
