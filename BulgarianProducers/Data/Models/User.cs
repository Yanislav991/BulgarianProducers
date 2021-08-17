using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BulgarianProducers.Data.Models
{
    public class User : IdentityUser
    {
        public string Information { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}
