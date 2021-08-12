using BulgarianProducers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianProducers.Test.Data
{
    public static class AgriculturalEvents
    {
        public static IEnumerable<AgriculturalEvent> TenEvents
           => Enumerable.Range(0, 10).Select(x => new AgriculturalEvent());
           
    }
}
