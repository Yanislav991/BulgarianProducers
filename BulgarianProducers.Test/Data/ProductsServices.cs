using BulgarianProducers.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace BulgarianProducers.Test.Data
{
    public static class ProductsServices
    {
        public static IEnumerable<ProductsAndServicesListingModel> TenEntities
            =>Enumerable.Range(0,10).Select(i => new ProductsAndServicesListingModel 
            {
            });
    }
}
