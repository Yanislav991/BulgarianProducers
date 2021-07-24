using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Models.Services;
using BulgarianProducers.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly BulgarianProducersDbContext data;
        private readonly IMapper mapper;

        public ServiceTypeService(IMapper mapper, BulgarianProducersDbContext data)
        {
            this.mapper = mapper;
            this.data = data;
        }

        public bool CheckForServiceTypeById(int id)
        {
            return data.ServiceTypes.Any(x => x.Id == id);
        }

        public List<ServiceTypeModel> GetServiceTypes()
        => data.ServiceTypes
            .Select(c => new ServiceTypeModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
    }
}
