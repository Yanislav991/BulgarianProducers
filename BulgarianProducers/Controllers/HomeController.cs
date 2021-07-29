using BulgarianProducers.Data.Models;
using BulgarianProducers.Models;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IGetServicesAndProductsService getServicesAndProducts;
        private readonly UserManager<User> userManager;

        public HomeController(IGetServicesAndProductsService getServicesAndProducts, UserManager<User> userManager = null)
        {

            this.getServicesAndProducts = getServicesAndProducts;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //var lastSixProducts = getServicesAndProducts.GetServicesAndProducts()
                //.Take(6).ToList();

            return View();
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
        public async Task<IActionResult> Mine(ProductsAndServicesQueryModel queryModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var queryResult = getServicesAndProducts.GetServicesAndProducts(
                queryModel.SearchTerm,
                queryModel.Sorting,
                queryModel.ShowProducts,
                queryModel.ShowServices,
                queryModel.CurrentPage,
                userId);
            return this.View(queryResult.ProductsAndServices);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
