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
        public async Task Create([FromBody] OrderInputModel model)
            => await this.orderService.AddOrder(model);
    }
}
