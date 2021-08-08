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
    }
}
