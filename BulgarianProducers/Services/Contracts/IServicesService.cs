using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Services;

namespace BulgarianProducers.Services.Contracts
{
    public interface IServicesService
    {
        public void AddService(AddServiceFormModel serviceFormModel);
        public ServiceViewModel GetService(int id);
        public Service GetDataService(int id);
        public bool Edit(
            int id,
            string name, 
            decimal price,
            string description,
            int categoryId,
            string imageUrl, 
            int? hours);
        void DeleteProduct(Service service);
    }
}
