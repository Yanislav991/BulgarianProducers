using BulgarianProducers.Data;
using BulgarianProducers.Models.Products;
using BulgarianProducers.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly BulgarianProducersDbContext data;

        public CategoriesService(BulgarianProducersDbContext data)
        {
            this.data = data;
        }

        public bool CheckForCategoryById(int categoryId)
        {
            return this.data.Categories.Any(x => x.Id == categoryId);
        }

        //AddMapper
        public List<ProductsCategoryModel> GetCategories()
         => this.data.Categories
            .Select(c => new ProductsCategoryModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
    }
}
