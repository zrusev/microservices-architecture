namespace Admin.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts.Statistics;
    using System.Threading.Tasks;

    public class StatisticsController : AdministrationController
    {
        private readonly IStatisticsService statistics;

        public StatisticsController(IStatisticsService statistics)
            => this.statistics = statistics;

        public async Task<IActionResult> Index()
            => View(await this.statistics.BoughtProducts());
    }
}
