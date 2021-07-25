using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BulgarianProducers.Data.Models
{
    public class AgriculturalEvent
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]       
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string Place { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<EventImage> EventImages { get; set; } = new HashSet<EventImage>();   
    }
}
