﻿namespace Statistics.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Statistics.Services.Contracts;
    using Statistics.Services.Models;
    using StoreApi.Services.Helpers;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Threading.Tasks;

    public class BoughtProductsController : ApplicationController
    {
        private readonly IBoughtProductService boughtProductService;

        public BoughtProductsController(IBoughtProductService boughtProductService)
            => this.boughtProductService = boughtProductService;

        [HttpGet]
        [Route(Id)]
        public async Task<IActionResult> BoughtProducts(int userId)
            => QueryResultExtensions.ToActionResult(
                await this.boughtProductService.GetBoughtProducts(userId));

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(TopBoughtProducts))]
        public async Task<IActionResult> TopBoughtProducts()
            => QueryResultExtensions.ToActionResult(
                QueryResult<BoughtProductListOutputModel>.Suceeded(
                    new BoughtProductListOutputModel
                    {
                        Ids = await this.boughtProductService.TopBoughtProducts()
                    }));

        [HttpGet]
        [Route(nameof(TotalBoughtProducts))]
        public async Task<IActionResult> TotalBoughtProducts()
            => QueryResultExtensions.ToActionResult(
                await this.boughtProductService.TotalBoughtProducts());
    }
}