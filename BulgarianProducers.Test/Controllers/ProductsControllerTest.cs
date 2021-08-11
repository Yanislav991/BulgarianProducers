using BulgarianProducers.Controllers;
using BulgarianProducers.Data.Models;
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
        public void DetailsActionShouldReturnView()
            => MyController<ProductsController>
            .Instance(x=>x.WithUser())
            .Calling(c => c.Details(1, true))
            .ShouldHave()
            .ActionAttributes(a=>a.RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(d=>d.WithSet<Product>(d=>d.Any(x=>x.Id ==1)))
            .AndAlso()
            .ShouldReturn()
            .View();
    }
}
