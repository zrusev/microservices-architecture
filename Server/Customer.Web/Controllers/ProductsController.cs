﻿namespace Customer.Web.Controllers
{
    using Customer.Services.Contracts;
    using Customer.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StoreApi.Services.Helpers;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;

    public class ProductsController : ApplicationController
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly ICategoryService categoryService;
        private readonly IManufacturerService manufacturerService;

        public ProductsController(ILogger<ProductsController> logger,
            IProductService productService,
            ICustomerService customerService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService)
        {
            this.logger = logger;
            this.productService = productService;
            this.customerService = customerService;
            this.categoryService = categoryService;
            this.manufacturerService = manufacturerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] int page, 
            string category, 
            string manufacturer, 
            string name)
        {
            var totalProducts = await this.productService.Total(HttpUtility.UrlDecode(category),
                HttpUtility.UrlDecode(manufacturer),
                HttpUtility.UrlDecode(name));

            var products = await this.productService.GetListings(page,
                HttpUtility.UrlDecode(category),
                HttpUtility.UrlDecode(manufacturer),
                HttpUtility.UrlDecode(name));

            return QueryResultExtensions.ToActionResult(
                    QueryResult<ProductSearchOutputModel>.Suceeded(
                        new ProductSearchOutputModel 
                        { 
                            Page = page,
                            TotalProducts = totalProducts,
                            Products = products
                        }));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Details))]
        public async Task<IActionResult> Details([FromQuery] int id, string name)
            => QueryResultExtensions.ToActionResult(
                await this.productService.GetDetails(id, 
                    HttpUtility.UrlDecode(name)));

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(ByIds))]
        public async Task<IActionResult> ByIds([FromQuery] int[] ids)
            => QueryResultExtensions.ToActionResult(
                QueryResult<IEnumerable<ProductOutputListModel>>.Suceeded(
                    await this.productService.GetDetails(ids)));

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(ProductInputModel model)
        {
            var category = await this.categoryService.Find(model.CategoryId);
            if (category == null)
            {
                this.logger.LogInformation($"Category with ID:{model.CategoryId} does not exist.");

                return QueryResultExtensions.ToActionResult(
                    QueryResult.Failed(Errors.Log("InvalidCategory", "Category does not exist")));
            }

            var manufacturer = await this.manufacturerService.Find(model.ManufacturerId);
            if (manufacturer == null)
            {
                this.logger.LogInformation($"Manufacturer with ID:{model.ManufacturerId} does not exist.");
                
                return QueryResultExtensions.ToActionResult(
                    QueryResult.Failed(Errors.Log("InvalidManufacturer", "Manufacturer does not exist")));
            }

            model.CategoryId = category.Id;
            model.ManufacturerId = manufacturer.Id;

            return QueryResultExtensions.ToActionResult( 
                await this.productService.Create(model));
        }

        [HttpPut]
        [Route(Id)]
        public async Task<IActionResult> Edit(int id, ProductInputModel model)
        {
            //ToDo: preliminary checks

            var product = await this.productService.Find(id);

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Url = model.Url;
            product.Image_url = model.Url;
            product.CategoryId = model.CategoryId;
            product.ManufacturerId = model.ManufacturerId;

            await this.productService.SaveToDb(product);

            return QueryResultExtensions.ToActionResult(
                QueryResult.Success);
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult<bool>> Delete(int id)
            => await this.productService.Delete(id);
    }
}