namespace Statistics.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Statistics.Services.Contracts;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Threading.Tasks;

    public class SeenProductsController : ApplicationController
    {
        private readonly ISeenProductService seenProductService;

        public SeenProductsController(ISeenProductService seenProductService) 
            => this.seenProductService = seenProductService;

        [HttpGet]
        [Route(Id)]
        public async Task<IActionResult> TotalViews(int id)
            => QueryResultExtensions.ToActionResult(
                await this.seenProductService.GetTotalVisits(id));


        [HttpPut]
        [Route(Id)]
        public async Task<IActionResult> IncrementViews(int id)
            => QueryResultExtensions.ToActionResult(
                await this.seenProductService.AddVisits(id));
    }
}
