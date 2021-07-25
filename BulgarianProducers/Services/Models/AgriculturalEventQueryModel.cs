using BulgarianProducers.Models.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Models
{
    public class AgriculturalEventQueryModel
    {
        
        [Display(Name ="Въведи име на събитието или таг")]
        public string SearchTerm { get; set; }
        [Display(Name ="Местоположение")]
        public string Place { get; set; }
        [Display(Name ="Да започва след")]
        public DateTime StartAfter { get; set; }
        [Display(Name ="Да приключи преди")]
        public DateTime EndBefore { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalEventsCount { get; set; }
        public List<AgriculturalEventInfoModel> Events { get; set; }
        public EventSorting Sorting { get; set; }

    }
}
