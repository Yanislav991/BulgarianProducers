using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Data.Models
{
    public class User : IdentityUser
    {
        public string Information { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}
