using BulgarianProducers.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Contracts
{
    public interface IServiceTypeService
    {
        public List<ServiceTypeModel> GetServiceTypes();
        public bool CheckForServiceTypeById(int id);
    }
}
