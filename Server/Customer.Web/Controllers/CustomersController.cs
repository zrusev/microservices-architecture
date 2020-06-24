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
        {
            this.customerService = customerService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(CreateCustomerInputModel model)
            => QueryResultExtensions.ToActionResult(
                await this.customerService.CreateCustomer(model,
                    User.GetNameIdentifier(),
                    User.GetEmail()));
    }
}
