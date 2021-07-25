using BulgarianProducers.Models.Products;
using BulgarianProducers.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BulgarianProducers.Controllers
{
    public class ProductsController:Controller
    {
        private readonly IProductService productService;
        private readonly ICategoriesService categoriesService;
        public ProductsController(IProductService productService, ICategoriesService categoriesService)
        {
            this.productService = productService;
            this.categoriesService = categoriesService;
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
        public IActionResult Add(AddProductFormModel product)
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
            this.productService.AddProduct(product);
            return this.Redirect("/");
        }

    }
}
