﻿using AutoMapper;
using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Services;
using BulgarianProducers.Services.Contracts;
using System;
using System.Linq;

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
            var service = this.GetDataService(id);
            var serviceToShow = mapper.Map<ServiceViewModel>(service);
            return serviceToShow;
        }
        public Service GetDataService(int id)
        {
            return this.data.Services.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Edit(
            int id,
            string name,
            decimal price,
            string description,
            int categoryId,
            string imageUrl,
            int? hours)
        {
            var service = this.data.Services.Find(id);
            if (service == null)
            {
                return false;
            }
            service.Name = name;
            service.Price = price;
            service.Description = description;
            service.ServiceTypeId = categoryId;
            service.ImageUrl = imageUrl;
            service.TimeNeeded = TimeSpan.Zero;
            service.TimeNeeded.Value.Add(new TimeSpan(hours ?? 0, 0, 0));
            this.data.SaveChanges();
            return true;
        }

        public void DeleteProduct(Service service)
        {
            this.data.Services.Remove(service);
            this.data.SaveChanges();
        }
    }
}
