namespace Customer.Web.Controllers
{
    using Customer.Services.Contracts;
    using Customer.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StoreApi.Services.Helpers;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Threading.Tasks;

    public class ProductsController : ApplicationController
    {
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly ICategoryService categoryService;
        private readonly IManufacturerService manufacturerService;

        public ProductsController(IProductService productService,
            ICustomerService customerService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService)
        {
            this.productService = productService;
            this.customerService = customerService;
            this.categoryService = categoryService;
            this.manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<ActionResult<ProductSearchOutputModel>> Search([FromQuery] int page)
        {
            var totalPages = await this.productService.Total();

            var products = await this.productService.GetListings(page);

            return QueryResultExtensions.ToActionResult<ProductSearchOutputModel>(
                    (dynamic)QueryResult<ProductSearchOutputModel>.Suceeded(
                        new ProductSearchOutputModel 
                        { 
                            Page = page,
                            TotalPages = totalPages,
                            Products = products
                        }));
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<ProductOutputModel>> Details(int id)
            => QueryResultExtensions.ToActionResult(
                await (dynamic)this.productService.GetDetails(id));

        public async Task<IActionResult> Create(ProductInputModel model)
        {
            var category = await this.categoryService.Find(model.CategoryId);

            if (category == null)
            {
                IdentityError[] errors = new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code = "InvalidCategory",
                        Description = "Category does not exist"
                    }
                };

                return QueryResultExtensions.ToActionResult(
                    QueryResult.Failed(errors));
            }

            var manufacturer = await this.manufacturerService.Find(model.ManufacturerId);

            if (manufacturer == null)
            {
                IdentityError[] errors = new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code = "InvalidManufacturer",
                        Description = "Manufacturer does not exist"
                    }
                };

                return QueryResultExtensions.ToActionResult(
                    QueryResult.Failed(errors));
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

            await this.productService.SaveToDb();

            return QueryResultExtensions.ToActionResult(QueryResult.Success);
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult<bool>> Delete(int id)
            => await this.productService.Delete(id);
    }
}