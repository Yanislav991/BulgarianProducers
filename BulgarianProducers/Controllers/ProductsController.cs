using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Controllers
{
    public class ProductsController:Controller
    {
        private readonly BulgarianProducersDbContext data;
        public ProductsController(BulgarianProducersDbContext data)
        {
            this.data = data;
        }
 
        public IActionResult Details(string id) 
        {
            
        
            var productToShow = data.Products.Where(x => x.Id == int.Parse(id)).Select(product => new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Name = product.Name,
                Category = product.Category.Name
            }).FirstOrDefault();
            if (productToShow == null)
            {
                //Soon will add ErrorView;
                return this.BadRequest();
            }
            return this.View(productToShow);
        }
        public IActionResult Add() 
        {
            
            return this.View(new AddProductFormModel { Categories = GetCategories()});
        }
        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            if (!this.data.Categories.Any(x => x.Id == product.CategoryId)) 
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid) 
            {
                product.Categories = GetCategories();
                return View(product);
            }
            var dataproduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = data.Categories.FirstOrDefault(x => x.Id == product.CategoryId)
            };
            data.Products.Add(dataproduct);
            data.SaveChanges();
            return this.Redirect("/");
        }
        private IEnumerable<ProductsCategoryModel> GetCategories()
             => this.data.Categories
            .Select(c => new ProductsCategoryModel 
            { 
                Id = c.Id,
                Name = c.Name 
            }).ToList();
    }
}
