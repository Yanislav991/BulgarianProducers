using System.ComponentModel.DataAnnotations;

namespace BulgarianProducers.Data.Models
{
    public class EventImage
    {
        public int Id { get; set; }
        public int AgriculturalEventId { get; set; }
        [Url]
        public string Url { get; set; }
        public AgriculturalEvent AgriculturalEvent { get; set; }
    }
}