using BulgarianProducers.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Contracts
{
    public interface IServicesService
    {
        public void AddService(AddServiceFormModel serviceFormModel);
        public ServiceViewModel GetService(int id);
    }
}
