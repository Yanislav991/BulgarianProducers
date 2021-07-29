using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Products;

namespace BulgarianProducers.Services.Contracts
{
    public interface IProductService
    {
        public ProductViewModel GetProduct(int id);
        public Product GetDataProduct(int id);
        public void AddProduct(AddProductFormModel product);
        public void DeleteProduct(Product product);
        bool Edit(int id, string name, decimal price, string description, int categoryId, string imageUrl);
    }
}
