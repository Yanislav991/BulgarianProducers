using BulgarianProducers.Models.Products;

namespace BulgarianProducers.Services.Contracts
{
    public interface IProductService
    {
        public ProductViewModel GetProduct(int id);
        public void AddProduct(AddProductFormModel product);

    }
}
