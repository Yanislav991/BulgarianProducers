using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianProducers.Infrastructure.Constants;

namespace BulgarianProducers.Models
{
    public class ProductsAndServicesApiRequest
    {
        public string SearchTerm { get; set; }
        public ProductsAndServicesSorting Sorting { get; set; }
        public bool ShowProducts { get; set; }
        public bool ShowServices { get; set; }
        public int CurrentPage { get; set; } = 1;
        

    }
}
