using AutoMapper;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        public AgriculturalEventsController(
            IEventsService eventService, 
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.eventsService = eventService;
            this.mapper = mapper;
            this.userManager = userManager;
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
        public IActionResult Edit(int id)
        {
            var @event = this.eventsService.GetEventData(id);
            if (@event == null)
            {
                return this.BadRequest();
            }
            
            var eventToEdit = this.mapper.Map<AddAgriculturalEventFormModel>(@event);
            return this.View(eventToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm]AddAgriculturalEventFormModel @event)
        {
            if (!this.ModelState.IsValid)
            {
                //var eventToEdit = this.mapper.Map<AddAgriculturalEventFormModel>(@event);
                return this.View(@event);
            }
            var user = await this.userManager.GetUserAsync(this.User);

            this.eventsService.Edit(id,
                @event.Name,
                @event.StartDate,
                @event.Description,
                @event.EndDate,
                @event.Image1,
                @event.Image2,
                @event.Image3,
                @event.Place);
            return this.Redirect("/Admin/AgriculturalEvents/AllEvents");
        }
    }
}
