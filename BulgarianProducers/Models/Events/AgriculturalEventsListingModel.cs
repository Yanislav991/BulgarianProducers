using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Models.Events
{
    public class AgriculturalEventsListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IEnumerable<string> ImageUrls { get; set; }
    }
}
