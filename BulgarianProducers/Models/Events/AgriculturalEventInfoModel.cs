using System.Collections.Generic;

namespace BulgarianProducers.Models.Events
{
    public class AgriculturalEventInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
