using BulgarianProducers.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Contracts
{
    public interface IProductService
    {
        public ProductViewModel GetProduct(int id);
        public void AddProduct(AddProductFormModel product);

    }
}
