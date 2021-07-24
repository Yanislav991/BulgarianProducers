using BulgarianProducers.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Contracts
{
    public interface IEventsService
    {
        public IEnumerable<AgriculturalEventInfoModel> GetEvents();
        public AgriculturalEventInfoModel GetEventById(int id);
        public void AddEvent(AddAgriculturalEventFormModel @event);
    }
}
