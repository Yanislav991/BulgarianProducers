using BulgarianProducers.Data;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services
{
    public class GetServicesAndProductsService : IGetServicesAndProductsService
    {
        private readonly BulgarianProducersDbContext data;

        public GetServicesAndProductsService(BulgarianProducersDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<ProductsAndServicesListingModel> GetServicesAndProducts()
        {
            var listingEntities = data.Products.Select(x => new ProductsAndServicesListingModel
            {
                Description =x.Description,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name =x.Name,
                Price = x.Price,
                IsProduct = true,
                CreatedOn = x.CreatedDate
            }).ToList();

            listingEntities.AddRange(data.Services.Select(x => new ProductsAndServicesListingModel
            {
                Description = x.Description,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price,
                IsProduct = false,
                CreatedOn = x.CreatedDate
            }).ToList());
            listingEntities.OrderByDescending(x => x.CreatedOn).ToList();
            return listingEntities;
        }
    }
}
