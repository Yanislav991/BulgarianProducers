using BulgarianProducers.Models;
using BulgarianProducers.Services.Models;

namespace BulgarianProducers.Services.Contracts
{
    public interface IGetServicesAndProductsService
    {
        public ProductsAndServicesQueryModel GetServicesAndProducts(
            string searchTerm,
            ProductsAndServicesSorting sorting,
            bool showProducts,
            bool showServices,
            int currentPage);
        
    }
}
