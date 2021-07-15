using BulgarianProducers.Data;
using BulgarianProducers.Models;
using BulgarianProducers.Models.Products;
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
        private readonly BulgarianProducersDbContext data;

        public HomeController(BulgarianProducersDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            var lastSixProducts = data.Products
                .OrderByDescending(x=>x.Id)
                .Take(6)
                .Select(x => new ProductsListingViewModel
            {
                Category = x.Category.Name,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name =x.Name
            }).ToList();

            return View(lastSixProducts);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
