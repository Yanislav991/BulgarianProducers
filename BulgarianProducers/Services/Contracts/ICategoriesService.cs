using BulgarianProducers.Models.Products;
using System.Collections.Generic;

namespace BulgarianProducers.Services.Contracts
{
    public interface ICategoriesService
    {
        public List<ProductsCategoryModel> GetCategories();
        public bool CheckForCategoryById(int categoryId);
    }
}
