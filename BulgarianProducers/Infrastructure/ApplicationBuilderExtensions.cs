using BulgarianProducers.Data;
using BulgarianProducers.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
