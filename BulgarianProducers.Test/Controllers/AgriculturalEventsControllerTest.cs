using BulgarianProducers.Controllers;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;
using static BulgarianProducers.Test.Data.AgriculturalEvents;

namespace BulgarianProducers.Test.Controllers
{
    public class AgriculturalEventsControllerTest
    {
        [Fact]
        public void AllEventsShouldReturnViewModel()
            => MyController<AgriculturalEventsController>
                .Instance()
                    .WithData(TenEvents)
                    .WithUser()
                .Calling(c => 
                         c.AllEvents(new AgriculturalEventQueryModel()))
                    .ShouldReturn()
                .View(v => 
                      v.WithModelOfType<AgriculturalEventQueryModel>());

        [Fact]
        public void InfoShouldReturnView()
            => MyController<AgriculturalEventsController>
                .Instance()
                    .WithData(new AgriculturalEvent() { Id = 1 })
                .Calling(c =>
                     c.Info(1))
                    .ShouldHave()
                        .ActionAttributes(x => x.RestrictingForAuthorizedRequests())
                .AndAlso()
                    .ShouldReturn()
                .View(v =>
                  v.WithModelOfType<AgriculturalEventInfoModel>());

        [Fact]
        public void InfoShouldReturnBadRequestWithInvalidEvent()
           => MyController<AgriculturalEventsController>
               .Instance()
                   .WithoutData()
               .Calling(c =>
                    c.Info(1))
                   .ShouldHave()
                       .ActionAttributes(x => x.RestrictingForAuthorizedRequests())
               .AndAlso()
                   .ShouldReturn()
               .BadRequest();



    }
}
