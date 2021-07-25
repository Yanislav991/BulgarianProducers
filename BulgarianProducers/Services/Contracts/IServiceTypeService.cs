using BulgarianProducers.Models.Services;
using System.Collections.Generic;

namespace BulgarianProducers.Services.Contracts
{
    public interface IServiceTypeService
    {
        public List<ServiceTypeModel> GetServiceTypes();
        public bool CheckForServiceTypeById(int id);
    }
}
