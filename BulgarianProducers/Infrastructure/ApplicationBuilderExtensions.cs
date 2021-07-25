using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BulgarianProducers.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data =scopedServices.ServiceProvider.GetService<BulgarianProducersDbContext>();
            data.Database.Migrate();
            SeedCategories(data);
            SeedServiceTypes(data);
            return app;
        }

        private static void SeedCategories(BulgarianProducersDbContext data) 
        {
            if (data.Categories.Any()) 
            {
                return;
            }
            data.AddRange(new Category[]
            {
                new Category{Name ="Foods"},
                new Category{Name ="Drinks"},
                new Category{Name ="Other"}
            });
            data.SaveChanges();
        }
        private static void SeedServiceTypes(BulgarianProducersDbContext data)
        {
            if (data.ServiceTypes.Any())
            {
                return;
            }
            data.AddRange(new ServiceType[]
            {
                new ServiceType{Name ="Transport"},
                new ServiceType{Name ="Moving Services"},
                new ServiceType{Name ="Craftsman"},
                new ServiceType{Name ="Painting"}
            });
            data.SaveChanges();
        }
    }
}
