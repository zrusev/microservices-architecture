namespace Customer.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;
    using Services.Models;
    using StoreApi.Services.Helpers;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ManufacturersController : ApplicationController
    {
        private readonly ILogger<ManufacturersController> logger;
        private readonly IManufacturerService manufacturerService;

        public ManufacturersController(ILogger<ManufacturersController> logger,
            IManufacturerService manufacturerService)
        {
            this.logger = logger;
            this.manufacturerService = manufacturerService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Top))]
        public async Task<IActionResult> Top()
         => QueryResultExtensions.ToActionResult(
             QueryResult<IEnumerable<ManufacturerResultOutputModel>>.Suceeded(
                 await this.manufacturerService.Top()
                ));
    }
}
