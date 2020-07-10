namespace Customer.Gateway.Web.Controllers
{
    using AutoMapper;
    using Customer.Gateway.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using StoreApi.Services.Helpers;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsController : ApplicationController
    {
        private readonly ITopBoughtProductsService topBoughtProductsService;
        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public ProductsController(ITopBoughtProductsService topBoughtProductsService,
            IProductsService productsService,
            IMapper mapper)
        {
            this.topBoughtProductsService = topBoughtProductsService;
            this.productsService = productsService;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Top))]
        public async Task<IActionResult> Top()
        {
            var topIdsResult = await this.topBoughtProductsService.TopBoughtProducts();

            var ids = topIdsResult.Ids.Select(i => i.ProductId);

            var products = await this.productsService.GetProducts(ids);

            return QueryResultExtensions.ToActionResult(
                QueryResult<IEnumerable<ProductOutputListModel>>.Suceeded(products));
        }
    }
}