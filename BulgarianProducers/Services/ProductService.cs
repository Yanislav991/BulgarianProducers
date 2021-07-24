using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Products;
using BulgarianProducers.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services
{
    public class ProductService : IProductService
    {
        private readonly BulgarianProducersDbContext data;
        private readonly IMapper mapper;
        public ProductService(BulgarianProducersDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void AddProduct(AddProductFormModel product)
        {
            var dataProduct = mapper.Map<Product>(product);
            data.Products.Add(dataProduct);
            data.SaveChanges();
        }

        public ProductViewModel GetProduct(int id)
        {
            var product = data.Products.Where(x => x.Id == id)
          .FirstOrDefault();
            var productToShow = mapper.Map<ProductViewModel>(product);
            return productToShow;
        }
    }
}
