using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Controllers
{
    public class ServicesController : Controller
    {
        private readonly BulgarianProducersDbContext data;

        public ServicesController(BulgarianProducersDbContext context)
        {
            this.data = context;
        }

        public IActionResult Add()
        {
            return this.View(new AddServiceFormModel { ServiceTypes = GetServiceTypes() });
        }
        [HttpPost]
        public IActionResult Add(AddServiceFormModel service) 
        {
            if (!data.ServiceTypes.Any(x => x.Id == service.ServiceTypeId)) 
            {
                this.ModelState.AddModelError(nameof(service.ServiceTypeId), "Category does not exist.");
            }
            if (!ModelState.IsValid) 
            {
                service.ServiceTypes = GetServiceTypes();
                return this.View(service);
            }
            var ts = TimeSpan.FromHours((double)service.TimeNeeded);
            var validService = new Service()
            {
                AdditionalInformation = service.AdditionalInformation,
                ImageUrl = service.ImageUrl,
                Name = service.Name,
                Price = service.Price,
                ServiceTypeId = service.ServiceTypeId,
                TimeNeeded = ts,  
            };
            data.Services.Add(validService);
            data.SaveChanges();
            //Should fix it later;
            return this.Redirect("/Products/All");
        }
        private IEnumerable<ServiceTypeModel> GetServiceTypes()
        => data.ServiceTypes
            .Select(c => new ServiceTypeModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
    }
}
