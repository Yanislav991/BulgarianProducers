using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Areas.Admin.Controllers
{
    public class AgriculturalEventsController : AdminController
    {
        private readonly IEventsService eventsService;

        public AgriculturalEventsController(IEventsService eventService)
        {
            this.eventsService = eventService;
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
        public IActionResult Delete(int id) 
        {
            this.eventsService.Delete(id);
            return this.Redirect("/Admin/AgriculturalEvents/AllEvents/");
        }
    }
}
