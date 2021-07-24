using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult AllEvents()
        {
            var events = this.eventsService.GetEvents();

            if (events == null)
            {
                return this.Redirect("/AgriculturalEvents/Add");
            }
            return this.View(events);
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
