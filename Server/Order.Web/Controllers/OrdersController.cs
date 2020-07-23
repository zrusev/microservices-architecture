namespace Order.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Services.Models;
    using StoreApi.Web.Controllers;
    using StoreApi.Web.Infrastructure;
    using System.Threading.Tasks;

    public class OrdersController : ApplicationController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
            => this.orderService = orderService;
        
        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] OrderInputModel model)
            => QueryResultExtensions.ToActionResult(
                await this.orderService.AddOrder(model));
    }
}
