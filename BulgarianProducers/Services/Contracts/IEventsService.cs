using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Models;
using System;

namespace BulgarianProducers.Services.Contracts
{
    public interface IEventsService
    {
        public AgriculturalEventQueryModel GetEvents(string searchTerm, string place, DateTime startAfter, DateTime endBefore, int currentPage, EventSorting sorting);
        public AgriculturalEventInfoModel GetEventById(int id);
        public void AddEvent(AddAgriculturalEventFormModel @event);
        public void Delete(int id);
    }
}
