using BulgarianProducers.Controllers;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Products;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BulgarianProducers.Test.Controllers
{
    public class ProductsControllerTest
    {
        [Fact]
        public void DetaildShouldReturView()
            => MyController<ProductsController>
                .Instance()
                .WithData(new Product())
                .WithUser()
                .Calling(c => c.Details(1, true))
                .ShouldReturn()
            .View(v => v.WithModelOfType<ProductViewModel>());
        [Fact]
        public void DetaildShouldReturBadRequestIfInjectedService()
            => MyController<ProductsController>
                .Instance()
                .WithData(new Product())
                .WithUser()
                .Calling(c => c.Details(1, false))
                .ShouldReturn()
            .BadRequest();

        [Fact]
        public void AddOnGetShouldReturnView()
        => MyController<ProductsController>
            .Instance()
            .Calling(c => c.Add())
            .ShouldHave()
            .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(v => v.WithModelOfType<AddProductFormModel>());

        //[Fact]
        //public void AddOnPostShouldRedirect()
        //    => MyController<ProductsController>
        //    .Instance()
        //    .WithData(new User() { Id = "1" })
        //    .Calling(c => c.Add(new AddProductFormModel()
        //    {
        //        UserId = "1",
        //        Description = "SomeDescription",
        //        ImageUrl = "https://www.somevalidurl.com/",
        //        Name = "SomeProduct",
        //        Price = (decimal)2.50,
        //        CategoryId = 1,
        //    }))
        //    .ShouldHave()
        //    .ValidModelState()
        //    .ActionAttributes(a =>
        //    a.RestrictingForHttpMethod(HttpMethod.Post)
        //     .RestrictingForAuthorizedRequests())
        //    .AndAlso()
        //    .ShouldReturn()
        //    .Redirect();

        [Fact]
        public void DeleteShouldRedirect()
        => MyController<ProductsController>
            .Instance()
            .WithData(new Product()
            {
                Id = 1,
                UserId = "TestId"
            },
            new User()
            {
                Id = "TestId"
            })
            .WithUser()
            .Calling(c => c.Delete(1, "TestId"))
            .ShouldReturn()
            .Redirect();
            
    }
}
