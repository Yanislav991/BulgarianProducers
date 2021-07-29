using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Models;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using System.Collections.Generic;
using System.Linq;
using static BulgarianProducers.Infrastructure.Constants;

namespace BulgarianProducers.Services
{
    public class GetServicesAndProductsService : IGetServicesAndProductsService
    {
        private readonly BulgarianProducersDbContext data;
        private readonly IMapper mapper;
        public GetServicesAndProductsService(BulgarianProducersDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public ProductsAndServicesQueryModel GetServicesAndProducts(
            string searchTerm,
            ProductsAndServicesSorting sorting,
            bool showProducts,
            bool showServices,
            int currentPage,
            string userId = null)
        {
            var listingEntities = new List<ProductsAndServicesListingModel>();
            if (showProducts && !showServices) 
            {
                listingEntities.AddRange(mapper.Map<List<ProductsAndServicesListingModel>>(data.Products.ToList()));
            }
            else if (showServices && !showProducts)
            {
                listingEntities.AddRange(mapper.Map<List<ProductsAndServicesListingModel>>(data.Services.ToList()));
            }
            else 
            {
                var products = data.Products.ToList();
                listingEntities.AddRange(mapper.Map<List<ProductsAndServicesListingModel>>(products));
                var services = data.Services.ToList();
                var listingServices = mapper.Map<List<ProductsAndServicesListingModel>>(services);
                listingEntities.AddRange(listingServices);
            }
            if (!string.IsNullOrWhiteSpace(searchTerm)) 
            {
                listingEntities = listingEntities
                    .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) 
                    || x.Description.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }
            var totalCount = listingEntities.Count();
            listingEntities = sorting switch
            {
                ProductsAndServicesSorting.Name => listingEntities.OrderBy(x => x.Name).ToList(),
                ProductsAndServicesSorting.Price => listingEntities.OrderBy(x => x.Price).ToList(),
                ProductsAndServicesSorting.FirstProducts =>listingEntities.OrderByDescending(x=>x.IsProduct).ToList(),
                ProductsAndServicesSorting.FirstServices => listingEntities.OrderBy(x=>x.IsProduct).ToList()
            };
            //Not good to be here 
            List<ProductsAndServicesListingModel> elementsToShow;
            if (userId == null)
            {
                elementsToShow = listingEntities
                    .Skip((currentPage - 1) * ProductsAndServicesPerPage)
                    .Take(ProductsAndServicesPerPage)
                    .ToList();
            }
            else 
            {
                elementsToShow = listingEntities.Where(x=>x.UserId == userId)
                    .Skip((currentPage - 1) * ProductsAndServicesPerPage)
                    .Take(ProductsAndServicesPerPage)
                    .ToList();
            }
            var queryResult = new ProductsAndServicesQueryModel()
            {
                CurrentPage= currentPage,
                SearchTerm = searchTerm,
                ShowProducts= showProducts,
                ShowServices= showServices,
                Sorting = sorting,
                TotalElementsCount= totalCount,
                ProductsAndServices = elementsToShow,
            };

            
            return queryResult;
        }

    }
}
