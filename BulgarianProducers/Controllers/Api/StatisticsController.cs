using BulgarianProducers.Services.Contracts;
using BulgarianProducers.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulgarianProducers.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
            => this.statisticsService = statisticsService;

        [HttpGet]
        public StatisticsModel GetStatistics() 
            => this.statisticsService.GetStatistics();
    }
}
