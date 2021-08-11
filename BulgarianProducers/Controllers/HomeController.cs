using BulgarianProducers.Data.Models;
using BulgarianProducers.Models;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly UserManager<User> userManager;
        private readonly IMemoryCache cache;

        public HomeController(
            IGetServicesAndProductsService getServicesAndProducts,
            UserManager<User> userManager = null,
            IMemoryCache cache = null)
        {

            this.getServicesAndProducts = getServicesAndProducts;
            this.userManager = userManager;
            this.cache = cache;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            const string latestEntitiesCacheKey = "LatestEntitiesCacheKey";
            var lastSixProducts = this.cache
                .Get<List<ProductsAndServicesListingModel>>(latestEntitiesCacheKey);
            if (lastSixProducts == null)
            {
                lastSixProducts = getServicesAndProducts
                    .GetLastSix();
                var memoryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));
                this.cache.Set(latestEntitiesCacheKey, lastSixProducts, memoryOptions);
            }
            return View(lastSixProducts);
        }
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Mine(ProductsAndServicesQueryModel queryModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var queryResult = getServicesAndProducts.GetServicesAndProducts(
                queryModel.SearchTerm,
                queryModel.Sorting,
                queryModel.ShowProducts,
                queryModel.ShowServices,
                queryModel.CurrentPage,
                user.Id);
            return this.View(queryResult.ProductsAndServices);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
