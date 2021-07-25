using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static BulgarianProducers.Infrastructure.Constants;
using System.Threading.Tasks;
using BulgarianProducers.Services.Models;

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
                .Where(x => x.Id == id).Select(x => new AgriculturalEventInfoModel
                {
                    Description = x.Description,
                    EndDate = x.EndDate.ToString("d"),
                    StartDate = x.StartDate.ToString("d"),
                    Id = x.Id,
                    ImageUrls = x.EventImages.Select(x => x.Url).ToList(),
                    Name = x.Name,
                    Place = x.Place
                })
                .FirstOrDefault();
            // var images = @event.EventImages.Select(x => x.Url).ToList();
            //var eventResult = mapper.Map<AgriculturalEventInfoModel>(@event);
            //eventResult.ImageUrls = images;
            return @event;


        }


        public List<AgriculturalEventInfoModel> GetEvents(string searchTerm,
            string place,
            DateTime startAfter,
            DateTime endBefore,
            int currentPage,
            EventSorting sorting)
        {
            var events = data.AgriculturalEvents.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                events = events
                    .Where(x => x.Description.ToLower().Contains(searchTerm)
                    || x.Name.ToLower().Contains(searchTerm));
            }
            if (!string.IsNullOrEmpty(place))
            {
                events = events.Where(x => x.Place.ToLower().Contains(place));
            }
            if (startAfter != default(DateTime))
            {
                events = events.Where(x => x.StartDate > startAfter);
            }
            if (endBefore != default(DateTime))
            {
                events = events.Where(x => x.EndDate < endBefore);
            }
            events = sorting switch
            {
                EventSorting.Name => events.OrderBy(x=>x.Name),
                EventSorting.Date => events.OrderBy(x=>x.StartDate)
            };
             
            var eventsToList = mapper.Map<List<AgriculturalEventInfoModel>>(events).ToList();


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
