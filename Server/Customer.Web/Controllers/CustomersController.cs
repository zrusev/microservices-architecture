namespace Customer.Web.Controllers
{
    using Customer.Services.Contracts;
    using Customer.Services.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Threading.Tasks;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ApplicationController
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
            => this.customerService = customerService;

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(CustomerCreateInputModel model)
            => QueryResultExtensions.ToActionResult(
                await this.customerService.CreateCustomer(model,
                    User.GetNameIdentifier(),
                    User.GetEmail()));

        [HttpGet]
        [Route(Id)]
        public async Task<IActionResult> Get([FromRoute] string id)
            => QueryResultExtensions.ToActionResult(
                await this.customerService.GetById(id));

        [HttpPut]
        [Route(Id)]
        public async Task<IActionResult> Edit(string id, EditCustomerInputModel model)
            => QueryResultExtensions.ToActionResult(
                await this.customerService.EditCustomer(id, model));

        [HttpGet]
        [Route(nameof(All))]
        public async Task<IActionResult> All()
            => QueryResultExtensions.ToActionResult(
                await this.customerService.GetAllCustomers());
    }
}