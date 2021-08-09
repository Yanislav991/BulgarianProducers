using AutoMapper;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Products;
using BulgarianProducers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BulgarianProducers.Controllers
{
    [Authorize]
    public class ProductsController:Controller
    {
        private readonly IProductService productService;
        private readonly ICategoriesService categoriesService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public ProductsController(
            IProductService productService,
            ICategoriesService categoriesService,
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.productService = productService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult Details(int id, bool isProduct) 
        {
            if (!isProduct) return this.BadRequest();
            var productToShow = this.productService.GetProduct(id);
            if (productToShow == null) return this.BadRequest();
            
            return this.View(productToShow);
        }
        public IActionResult Add() 
        { 
            return this.View(new AddProductFormModel { Categories = this.categoriesService.GetCategories()});
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductFormModel product)
        {
            if (!this.categoriesService.CheckForCategoryById(product.CategoryId)) 
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid) 
            {
                product.Categories = this.categoriesService.GetCategories();
                return View(product);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            product.UserId = user.Id;
            this.productService.AddProduct(product);
            return this.Redirect("/");
        }
        public async Task<IActionResult> Delete(int Id, string userId) 
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUserId = user.Id;
            var product = this.productService.GetDataProduct(Id);
            if (currentUserId !=userId) 
            {
                return this.BadRequest();
            }
            if (currentUserId != product.UserId) 
            {
                return this.BadRequest();
            }
            if (product == null) 
            {
                return this.BadRequest();
            }
            this.productService.DeleteProduct(product);
            return this.Redirect("/Home/Mine");
        }
        public IActionResult Edit(int id, string userId) 
        {
            var product = this.productService.GetDataProduct(id);
            if (product == null) 
            {
                return this.BadRequest();
            }
            if (product.UserId != userId) 
            {
                return this.Unauthorized();
            }
            var productToEdit = this.mapper.Map<AddProductFormModel>(product);
            productToEdit.Categories = this.categoriesService.GetCategories();
            return this.View(productToEdit); 
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddProductFormModel product) 
        {
            if (!this.categoriesService.CheckForCategoryById(product.CategoryId)) 
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }
            if (!this.ModelState.IsValid) 
            {
                var productToEdit = this.mapper.Map<AddProductFormModel>(product);
                productToEdit.Categories = this.categoriesService.GetCategories();
                return this.View(product);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUserId = user.Id;

            if (product.UserId != currentUserId) 
            {
                return this.Unauthorized();
            }
            this.productService.Edit(id,
                product.Name, 
                product.Price, 
                product.Description, 
                product.CategoryId,
                product.ImageUrl);
            return this.Redirect("/Home/Mine");
        }

    }
}
