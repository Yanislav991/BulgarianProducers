using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianProducers.Infrastructure.Constants;

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
        // to fix this controller
        public IActionResult AllEvents([FromQuery] AgriculturalEventQueryModel queryModel)
        {
            var events = this.eventsService.GetEvents(
                queryModel.SearchTerm,
                queryModel.Place,
                queryModel.StartAfter,
                queryModel.EndBefore,
                queryModel.CurrentPage,
                queryModel.Sorting);

            if (events == null)
            {
                return this.Redirect("/AgriculturalEvents/Add");
            }
            var query = new AgriculturalEventQueryModel();
            query.Events = events
                .Skip((queryModel.CurrentPage - 1) * EventsPerPage)
                .Take(EventsPerPage)
                .ToList();
            if (queryModel != null)
            {
                query.StartAfter = queryModel.StartAfter;
                query.CurrentPage = queryModel.CurrentPage == 0 ? 1 : queryModel.CurrentPage;
                query.EndBefore = queryModel.EndBefore;
                query.Place = queryModel.Place;
                query.SearchTerm = queryModel.SearchTerm;
                query.TotalEventsCount = events.Count();
                query.Sorting = queryModel.Sorting;
            }

            return this.View(query);
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
