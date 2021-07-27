using BulgarianProducers.Models;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Controllers.Api
{
    [ApiController]
    [Route("/api/productsAndServices")]
    public class ProductsAndServicesApiController:ControllerBase
    {
        private readonly IGetServicesAndProductsService getServicesAndProducts;

        public ProductsAndServicesApiController(IGetServicesAndProductsService getServicesAndProducts)
        {
            this.getServicesAndProducts = getServicesAndProducts;
        }

        [HttpGet]
        public ProductsAndServicesQueryModel All([FromQuery] ProductsAndServicesApiRequest queryModel)
            => this.getServicesAndProducts.GetServicesAndProducts(
                queryModel.SearchTerm,
                queryModel.Sorting, 
                queryModel.ShowProducts, 
                queryModel.ShowServices, 
                queryModel.CurrentPage);
    }
}
