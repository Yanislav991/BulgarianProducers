using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Products;
using BulgarianProducers.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BulgarianProducers.Services
{
    public class ProductService : IProductService
    {
        private readonly BulgarianProducersDbContext data;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public ProductService(BulgarianProducersDbContext data, IMapper mapper, UserManager<User> userManager)
        {
            this.data = data;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public void AddProduct(AddProductFormModel product)
        {
            var dataProduct = mapper.Map<Product>(product);
            data.Products.Add(dataProduct);
            data.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            this.data.Products.Remove(product);
            this.data.SaveChanges();
        }

        public bool Edit(int id, string name, decimal price, string description, int categoryId, string imageUrl)
        {
            var product = this.data.Products.Find(id);
            if (product == null) 
            {
                return false;
            }
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.CategoryId = categoryId;
            product.ImageUrl = imageUrl;
            this.data.SaveChanges();
            return true;
                
        }

        public Product GetDataProduct(int id)
        {
           var product =data.Products.Where(x => x.Id == id)
          .FirstOrDefault();
            return product;
        }

        public ProductViewModel GetProduct(int id)
        {
            var product = GetDataProduct(id);
            var productToShow = mapper.Map<ProductViewModel>(product);
            return productToShow;
        }
    }
}
