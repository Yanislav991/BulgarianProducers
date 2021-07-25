using BulgarianProducers.Models.Services;
using BulgarianProducers.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BulgarianProducers.Controllers
{
    public class ServicesController : Controller
    { 
        private readonly IServicesService servicesService;
        private readonly IServiceTypeService serviceTypeService;
        public ServicesController(IServicesService servicesService, IServiceTypeService serviceTypeService)
        {
            this.servicesService = servicesService;
            this.serviceTypeService = serviceTypeService;
        }

        public IActionResult Add()
        {
            return this.View(new AddServiceFormModel { ServiceTypes = this.serviceTypeService.GetServiceTypes() });
        }
        //Should be in service
        [HttpPost]
        public IActionResult Add(AddServiceFormModel service) 
        {
            if (!serviceTypeService.CheckForServiceTypeById(service.ServiceTypeId)) 
            {
                this.ModelState.AddModelError(nameof(service.ServiceTypeId), "Category does not exist.");
            }
            if (!ModelState.IsValid) 
            {
                service.ServiceTypes = this.serviceTypeService.GetServiceTypes();
                return this.View(service);
            }
            this.servicesService.AddService(service);
            return this.Redirect("/Home/All");
        }
        public IActionResult Details(int id, bool isProduct) 
        {
           
            if (isProduct) return this.BadRequest();
            var serviceToShow = servicesService.GetService(id);
            if (serviceToShow == null) return this.BadRequest();


            return this.View(serviceToShow);
        }

    }
}
