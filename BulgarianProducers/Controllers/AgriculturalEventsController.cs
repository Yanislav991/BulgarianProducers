using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulgarianProducers.Controllers
{
    public class AgriculturalEventsController : Controller
    {
        private readonly IEventsService eventsService;

        public AgriculturalEventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public IActionResult Add() => this.View();

        [HttpPost]
        public IActionResult Add(AddAgriculturalEventFormModel eventModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            this.eventsService.AddEvent(eventModel);
            return this.Redirect(nameof(AllEvents));

        }
        public IActionResult AllEvents([FromQuery] AgriculturalEventQueryModel queryModel)
        {
            var queryResult = this.eventsService.GetEvents(
                 queryModel.SearchTerm,
                 queryModel.Place,
                 queryModel.StartAfter,
                 queryModel.EndBefore,
                 queryModel.CurrentPage,
                 queryModel.Sorting);

            return this.View(queryResult);
        }


        public IActionResult Info(int id)
        {

            var @event = this.eventsService.GetEventById(id);
            if (@event == null)
            {
                return this.BadRequest();
            }
            return this.View(@event);
        }

    }
}
