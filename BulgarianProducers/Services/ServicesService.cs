using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Services;
using BulgarianProducers.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services
{
    public class ServicesService : IServicesService
    {
        private readonly BulgarianProducersDbContext data;
        private readonly IMapper mapper;

        public ServicesService(IMapper mapper, BulgarianProducersDbContext data)
        {
            this.mapper = mapper;
            this.data = data;
        }

        public void AddService(AddServiceFormModel serviceFormModel)
        {
            var ts = TimeSpan.FromHours((double)serviceFormModel.TimeNeeded);
            var validService = mapper.Map<Service>(serviceFormModel);
            validService.TimeNeeded = ts;
            data.Services.Add(validService);
            data.SaveChanges();
            
        }

        public ServiceViewModel GetService(int id)
        {
            var service = data.Services.Where(x => x.Id == id).FirstOrDefault();
            var serviceToShow = mapper.Map<ServiceViewModel>(service);
            return serviceToShow;
        }
    }
}
