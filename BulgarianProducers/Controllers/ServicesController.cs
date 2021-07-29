using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Services;
using BulgarianProducers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BulgarianProducers.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    { 
        private readonly IServicesService servicesService;
        private readonly IServiceTypeService serviceTypeService;
        private readonly UserManager<User> userManager;
        public ServicesController(IServicesService servicesService, IServiceTypeService serviceTypeService, UserManager<User> userManager)
        {
            this.servicesService = servicesService;
            this.serviceTypeService = serviceTypeService;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return this.View(new AddServiceFormModel { ServiceTypes = this.serviceTypeService.GetServiceTypes() });
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddServiceFormModel service) 
        {
            if (!serviceTypeService.CheckForServiceTypeById(service.ServiceTypeId)) 
            {
                this.ModelState.AddModelError(nameof(service.ServiceTypeId), "ServiceType does not exist.");
            }
            if (!ModelState.IsValid) 
            {
                service.ServiceTypes = this.serviceTypeService.GetServiceTypes();
                return this.View(service);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            service.UserId = user.Id;
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
