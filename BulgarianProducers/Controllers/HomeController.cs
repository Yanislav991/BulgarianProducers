using BulgarianProducers.Models;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulgarianProducers.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IGetServicesAndProductsService getServicesAndProducts;

        public HomeController(IGetServicesAndProductsService getServicesAndProducts)
        {

            this.getServicesAndProducts = getServicesAndProducts;
        }

        public IActionResult Index()
        {
            //var lastSixProducts = getServicesAndProducts.GetServicesAndProducts()
                //.Take(6).ToList();
            return View();
        }
        public IActionResult All(ProductsAndServicesQueryModel queryModel)
        {
            var queryResult = getServicesAndProducts.GetServicesAndProducts(
                queryModel.SearchTerm,
                queryModel.Sorting,
                queryModel.ShowProducts,
                queryModel.ShowServices,
                queryModel.CurrentPage);
            
            
            return this.View(queryResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
