using BulgarianProducers.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianProducers.Services.Contracts
{
    public interface IStatisticsService
    {
        public StatisticsModel GetStatistics();
    }
}
