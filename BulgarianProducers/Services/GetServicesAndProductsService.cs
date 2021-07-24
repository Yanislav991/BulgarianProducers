using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;
        public GetServicesAndProductsService(BulgarianProducersDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<ProductsAndServicesListingModel> GetServicesAndProducts()
        {
            var products = data.Products.ToList();
            var listingEntities = mapper.Map<List<ProductsAndServicesListingModel>>(products);
            var services = data.Services.ToList();
            var listingServices = mapper.Map<List<ProductsAndServicesListingModel>>(services);
            listingEntities.AddRange(listingServices);

            listingEntities.OrderByDescending(x => x.CreatedOn).ToList();
            return listingEntities;
        }
    }
}
