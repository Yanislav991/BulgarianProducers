using AutoMapper;
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
        private readonly IMapper mapper;
        public ServicesController(
            IServicesService servicesService,
            IServiceTypeService serviceTypeService,
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.servicesService = servicesService;
            this.serviceTypeService = serviceTypeService;
            this.userManager = userManager;
            this.mapper = mapper;
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
        public IActionResult Edit(int id, string userId)
        {
            var service = this.servicesService.GetDataService(id);
            if (service == null)
            {
                return this.BadRequest();
            }
            if (service.UserId != userId)
            {
                return this.Unauthorized();
            }
            var serviceToEdit = this.mapper.Map<AddServiceFormModel>(service);
            serviceToEdit.ServiceTypes = this.serviceTypeService.GetServiceTypes();
            return this.View(serviceToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddServiceFormModel service)
        {
            if (!this.serviceTypeService.CheckForServiceTypeById(service.ServiceTypeId))
            {
                this.ModelState.AddModelError(nameof(service.ServiceTypeId), "Service does not exist.");
            }
            if (!this.ModelState.IsValid)
            {
                var serviceToEdit = this.mapper.Map<AddServiceFormModel>(service);
                serviceToEdit.ServiceTypes = this.serviceTypeService.GetServiceTypes();
                return this.View(service);
            }
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUserId = user.Id;

            if (service.UserId != currentUserId)
            {
                return this.Unauthorized();
            }
            this.servicesService.Edit(id,
                service.Name,
                service.Price,
                service.AdditionalInformation,
                service.ServiceTypeId,
                service.ImageUrl,
                service.TimeNeeded);
            return this.Redirect("/Home/Mine");
        }
        public async Task<IActionResult> Delete(int Id, string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUserId = user.Id;
            var service = this.servicesService.GetDataService(Id);
            if (currentUserId != userId)
            {
                return this.BadRequest();
            }
            if (currentUserId != service.UserId)
            {
                return this.BadRequest();
            }
            if (service == null)
            {
                return this.BadRequest();
            }
            this.servicesService.DeleteProduct(service);
            return this.Redirect("/Home/Mine");

        }
    }
}
