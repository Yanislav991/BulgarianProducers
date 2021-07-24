using BulgarianProducers.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Contracts
{
    public interface ICategoriesService
    {
        public List<ProductsCategoryModel> GetCategories();
        public bool CheckForCategoryById(int categoryId);
    }
}
