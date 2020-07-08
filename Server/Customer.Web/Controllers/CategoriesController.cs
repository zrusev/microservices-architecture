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

    public class CategoriesController : ApplicationController
    {
        private readonly ILogger<CategoriesController> logger;
        private readonly ICategoryService categoryService;

        public CategoriesController(ILogger<CategoriesController> logger,
            ICategoryService categoryService)
        {
            this.logger = logger;
            this.categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Top))]
        public async Task<IActionResult> Top()
         => QueryResultExtensions.ToActionResult(
         QueryResult<IEnumerable<CategoryResultOutputModel>>.Suceeded(
             await this.categoryService.Top()
             ));
    }
}
