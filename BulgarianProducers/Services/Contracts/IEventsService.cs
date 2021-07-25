using BulgarianProducers.Models.Events;
using BulgarianProducers.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Contracts
{
    public interface IEventsService
    {
        public List<AgriculturalEventInfoModel> GetEvents(string searchTerm, string place, DateTime startAfter, DateTime endBefore, int currentPage, EventSorting sorting);
        public AgriculturalEventInfoModel GetEventById(int id);
        public void AddEvent(AddAgriculturalEventFormModel @event);
    }
}
