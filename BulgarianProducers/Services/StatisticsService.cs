using BulgarianProducers.Data;
using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly BulgarianProducersDbContext data;

        public StatisticsService(BulgarianProducersDbContext data)
        {
            this.data = data;
        }

        public StatisticsModel GetStatistics()
        {
            var model = new StatisticsModel();
            model.TotalProducts = this.data.Products.Count();
            model.TotalServices = this.data.Services.Count();
            model.TotalUsers = this.data.Users.Count();
            return model;
        }
    }
}
