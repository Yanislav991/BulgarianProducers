using BulgarianProducers.Controllers;
using BulgarianProducers.Services.Models;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BulgarianProducers.Test.Data.ProductsServices;
using static BulgarianProducers.Test.Data.AdminUser;
using BulgarianProducers.Data.Models;

namespace BulgarianProducers.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void MineShouldReturnToView()
            => MyController<HomeController>
            .Instance()
            .WithUser()
            .WithData(new User() 
            {
                Id= "TestId"
            })
            .Calling(c => c.Mine(new ProductsAndServicesQueryModel()
            {
                ProductsAndServices = TenEntities.ToList()
            }))
            .ShouldHave()
            .ActionAttributes(a =>
                     a.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void ActionAllShouldReturnView()
                => MyController<HomeController>
                .Instance()
                .Calling(x => x.All(new ProductsAndServicesQueryModel()))
            .ShouldReturn()
            .View(v => v.WithModelOfType<ProductsAndServicesQueryModel>());
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
             => MyController<HomeController>
                .Instance()
            .Calling(x => x.Index())
            .ShouldHave()
                .MemoryCache(cache =>
                    cache.ContainingEntry(entry =>
                        entry.WithKey("LatestEntitiesCacheKey")
                .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                .WithValueOfType<List<ProductsAndServicesListingModel>>()))
            .AndAlso()
            .ShouldReturn()
                .View(v => v.WithModelOfType<List<ProductsAndServicesListingModel>>());


        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();


    }
}
