using BulgarianProducers.Models.Services;

namespace BulgarianProducers.Services.Contracts
{
    public interface IServicesService
    {
        public void AddService(AddServiceFormModel serviceFormModel);
        public ServiceViewModel GetService(int id);
    }
}
