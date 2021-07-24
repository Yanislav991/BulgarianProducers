using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services
{
    public class EventService : IEventsService
    {
        private readonly BulgarianProducersDbContext data;
        private readonly IMapper mapper;
        public EventService(BulgarianProducersDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void AddEvent(AddAgriculturalEventFormModel eventModel)
        {
            var images = new List<EventImage>();
            //To find a way to do this better
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
            var eventToAdd = this.mapper.Map<AgriculturalEvent>(eventModel);
            eventToAdd.EventImages = images;
            eventToAdd.StartDate = ParseDate(eventModel.StartDate);
            eventToAdd.EndDate = ParseDate(eventModel.EndDate);
            data.AgriculturalEvents.Add(eventToAdd);
            data.SaveChanges();
        }

        public AgriculturalEventInfoModel GetEventById(int id)
        {
            var @event = data.AgriculturalEvents
                .Where(x => x.Id == id).Select(x=> new AgriculturalEventInfoModel 
                {
                    Description = x.Description,
                    EndDate = x.EndDate.ToString("d"),
                    StartDate = x.StartDate.ToString("d"),
                    Id = x.Id,
                    ImageUrls= x.EventImages.Select(x=>x.Url).ToList(),
                    Name=x.Name,
                    Place= x.Place
                })
                .FirstOrDefault();
           // var images = @event.EventImages.Select(x => x.Url).ToList();
            //var eventResult = mapper.Map<AgriculturalEventInfoModel>(@event);
           //eventResult.ImageUrls = images;
            return @event;


        }


        public IEnumerable<AgriculturalEventInfoModel> GetEvents()
        {
            var events = data.AgriculturalEvents.ToList();
            var eventsToList = mapper.Map<List<AgriculturalEventInfoModel>>(events);
            return eventsToList;
        }

        private DateTime ParseDate(string date) 
        {
            DateTime dateTime;
            DateTime
               .TryParseExact(date,
               "dd/MM/yyyy", CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out dateTime);
            return dateTime;
        }
    }
}
