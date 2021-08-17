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

        public void Delete(int id)
        {
            var @event = this.data.AgriculturalEvents.Where(x => x.Id == id).FirstOrDefault();
            this.data.AgriculturalEvents.Remove(@event);
            this.data.SaveChanges();
        }

        public bool Edit(
            int id, 
            string name,
            string startDate,
            string description,
            string endDate,
            string image1,
            string image2, 
            string image3,
            string place)
        {
            var @event = this.data.AgriculturalEvents.Find(id);
            if (@event == null) 
            {
                return false;
            }
            var oldImages = this.data.EventImages.Where(x => x.AgriculturalEventId == id);
            this.data.EventImages.RemoveRange(oldImages);
            this.data.SaveChanges();
            var eventImages = new List<EventImage>();
            eventImages.Add(new EventImage { Url = image1 });
            eventImages.Add(new EventImage { Url = image2 });
            eventImages.Add(new EventImage { Url = image3 });
            @event.Description = description;
            @event.Name = name;
            @event.Place = place;
            @event.StartDate = ParseDate(startDate);
            @event.EndDate = ParseDate(endDate);
            @event.EventImages = eventImages;
            this.data.SaveChanges();
            return true;
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

        public AgriculturalEvent GetEventData(int id)
        {
            return this.data.AgriculturalEvents.Find(id);
        }

        public AgriculturalEventQueryModel GetEvents(
            string searchTerm,
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
                EventSorting.Name => events.OrderBy(x => x.Name),
                EventSorting.Date => events.OrderBy(x => x.StartDate),
                _ => events.OrderBy(x=>x.Id)
            };
            
             
            var eventsToList = mapper
                .Map<List<AgriculturalEventInfoModel>>(events)
                .Skip((currentPage - 1) * EventsPerPage)
                .Take(EventsPerPage)
                .ToList();
            
            var queryModel = new AgriculturalEventQueryModel()
            {
                StartAfter = startAfter,
                CurrentPage = currentPage,
                EndBefore = endBefore,
                Events = eventsToList,
                Place = place,
                SearchTerm = searchTerm,
                Sorting = sorting,
                TotalEventsCount = events.Count()
            };

            return queryModel;
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
