using AutoMapper;
using BulgarianProducers.Data.Models;
using BulgarianProducers.Models.Events;
using BulgarianProducers.Models.Products;
using BulgarianProducers.Models.Services;
using BulgarianProducers.Services.Models;
using System.Linq;

namespace BulgarianProducers.Mapping
{
    public class BulgarianProducersProfile :Profile
    {
        public BulgarianProducersProfile()
        {
            this.CreateMap<Product, ProductsAndServicesListingModel>()
                .ForMember(x => x.IsProduct, x => x.MapFrom(x=> true))
                .ForMember(x=>x.UpdatedOn, x=>x.MapFrom(x=>x.UpdatedDate))
                .ForMember(x=>x.CreatedOn, x=>x.MapFrom(x=>x.CreatedDate));

            this.CreateMap<Service, ProductsAndServicesListingModel>()
                .ForMember(x => x.IsProduct, x => x.MapFrom(x=>false))
                .ForMember(x => x.UpdatedOn, x => x.MapFrom(x => x.UpdatedDate))
                .ForMember(x => x.CreatedOn, x => x.MapFrom(x => x.CreatedDate)); ;
            this.CreateMap<AddServiceFormModel, Service>()
                .ForMember(x => x.TimeNeeded, x => x.Ignore())
                .ForMember(x => x.Description, x => x.MapFrom(x => x.AdditionalInformation));
                
                

            this.CreateMap<Product, ProductViewModel>();
            this.CreateMap<AddProductFormModel, Product>().ReverseMap();

            this.CreateMap<Service, AddServiceFormModel>().ForMember(x => x.TimeNeeded, x => x
                  .MapFrom(x => x.TimeNeeded.HasValue ? x.TimeNeeded.Value.Hours : default(int)));

            this.CreateMap<Service, ServiceViewModel>()
                .ForMember(x=>x.TimeNeeded, x=>x
                .MapFrom(x=>x.TimeNeeded.HasValue ? x.TimeNeeded.Value
                .ToString(@"dd\.hh\:mm\:ss") : "Не е посочено време"));
            this.CreateMap<AddAgriculturalEventFormModel, AgriculturalEvent>()
                .ForMember(x=>x.StartDate, x=>x.Ignore())
                .ForMember(x=>x.EndDate, x=>x.Ignore());


            //This is not working for pictures idk why .. :/
            this.CreateMap<AgriculturalEvent, AgriculturalEventInfoModel>()
                .ForMember(x => x.ImageUrls, x => x.MapFrom(x => x.EventImages.Select(x => x.Url).ToList()))
                .ForMember(x => x.StartDate, x => x.MapFrom(x => x.StartDate.ToString("d")))
                .ForMember(x => x.EndDate, x => x.MapFrom(x => x.EndDate.ToString("d")));


        }
    }
}
