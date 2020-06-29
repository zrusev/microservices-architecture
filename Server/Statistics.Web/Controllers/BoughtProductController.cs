namespace Statistics.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Statistics.Services.Contracts;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Threading.Tasks;

    public class BoughtProductController : ApplicationController
    {
        private readonly IBoughtProductService boughtProductService;

        public BoughtProductController(IBoughtProductService boughtProductService)
            => this.boughtProductService = boughtProductService;

        public async Task<IActionResult> BoughtProducts(int userId)
            => QueryResultExtensions.ToActionResult(
                await this.boughtProductService.GetBoughtProducts(userId));
    }
}