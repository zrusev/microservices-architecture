namespace Statistics.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Statistics.Services.Contracts;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Threading.Tasks;

    public class SeenProductController : ApplicationController
    {
        private readonly ISeenProductService seenProductService;

        public SeenProductController(ISeenProductService seenProductService) 
            => this.seenProductService = seenProductService;

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> TotalViews(int id)
            => QueryResultExtensions.ToActionResult(
                await this.seenProductService.GetTotalVisits(id));
    }
}
