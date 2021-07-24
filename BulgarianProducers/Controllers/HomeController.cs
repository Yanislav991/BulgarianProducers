using BulgarianProducers.Data;
using BulgarianProducers.Models;
using BulgarianProducers.Models.Products;
using BulgarianProducers.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            var lastSixProducts = getServicesAndProducts.GetServicesAndProducts()
                .Take(6).ToList();
                

            return View(lastSixProducts);
        }
        public IActionResult All()
        {
            var productsToShow = getServicesAndProducts.GetServicesAndProducts();
            return this.View(productsToShow);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
