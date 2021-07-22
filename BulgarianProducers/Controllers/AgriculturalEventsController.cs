using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
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
        public readonly BulgarianProducersDbContext data;

        public AgriculturalEventsController(BulgarianProducersDbContext data)
        {
            this.data = data;
        }
  
        public IActionResult Add()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Add(AddAgriculturalEventFormModel eventModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            //To Fix
            var images = new List<EventImage>();
            if (eventModel.Image1 != null)
            {
                images.Add(new EventImage { Url = eventModel.Image1 });
            }
            if (eventModel.Image2 != null)
            {
                images.Add(new EventImage { Url = eventModel.Image2 });
            }
            if (eventModel.Image3 != null)
            {
                images.Add(new EventImage { Url = eventModel.Image3 });
            }
            DateTime startDate;
            DateTime endDate;
            DateTime
                .TryParseExact(eventModel.StartDate,
                "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out startDate);
            DateTime
               .TryParseExact(eventModel.EndDate,
               "dd/MM/yyyy", CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out endDate);
            var eventToAdd = new AgriculturalEvent()
            {
                Description = eventModel.Description,
                Place = eventModel.Place,
                Name = eventModel.Name,
                Images = images,
                StartDate = startDate,
                EndDate = endDate

            };
            data.AgriculturalEvents.Add(eventToAdd);
            data.SaveChanges();
            return this.Redirect(nameof(AllEvents));

        }
        public IActionResult AllEvents()
        {
            var events = data.AgriculturalEvents.Select(x => new AgriculturalEventsListingModel
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrls = x.Images.Select(x => x.Url).ToList(),
                Name = x.Name,
                Place = x.Place,
                StartDate = x.StartDate.ToString("d"),
                EndDate = x.EndDate.ToString("d")
            }).ToList();
            if (events == null) 
            {
                return this.Redirect("/AgriculturalEvents/Add");
            }
            return this.View(events);
        }


        public IActionResult Info(int id) 
        {

            var @event = data.AgriculturalEvents
                .Where(x=>x.Id == id)
                .Select(x => new AgriculturalEventInfoModel
            {
                Description = x.Description,
                EndDate = x.EndDate.ToString("d"),
                StartDate = x.StartDate.ToString("d"),
                ImagesUrls = x.Images.Select(x => x.Url).ToList(),
                Name = x.Name,
                Place = x.Place
            }).FirstOrDefault();
            if (@event == null)
            {
                return this.BadRequest();
            }
            return this.View(@event);
        }
       
    }
}
