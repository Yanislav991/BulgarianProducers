using BulgarianProducers.Models;
using BulgarianProducers.Services.Models;
using System.Collections.Generic;

namespace BulgarianProducers.Services.Contracts
{
    public interface IGetServicesAndProductsService
    {
        public ProductsAndServicesQueryModel GetServicesAndProducts(
            string searchTerm,
            ProductsAndServicesSorting sorting,
            bool showProducts,
            bool showServices,
            int currentPage,
            string userId = null);
        
    }
}
