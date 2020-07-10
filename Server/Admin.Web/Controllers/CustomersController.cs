namespace Admin.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts.Customers;
    using Services.Models.Customers;
    using System.Threading.Tasks;

    public class CustomersController : AdministrationController
    {
        private readonly ICustomersService customers;
        private readonly IMapper mapper;

        public CustomersController(ICustomersService customers, 
            IMapper mapper)
        {
            this.customers = customers;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
            => View(await this.customers.All());

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dealer = await this.customers.Details(id);

            var dealerForm = this.mapper.Map<CustomerFormModel>(dealer);

            return View(dealerForm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerFormModel model)
            => await this.Handle(
                async () => await this.customers
                    .Edit(id, this.mapper.Map<CustomerInputModel>(model)),
                success: RedirectToAction(nameof(Index)),
                failure: View(model));
    }
}