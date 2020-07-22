namespace Customer.Web.Messages
{
    using MassTransit;
    using Services.Contracts;
    using StoreApi.Web.Messages;
    using System.Threading.Tasks;

    public class OrderConsumer : IConsumer<OrderMessage>
    {
        private readonly IProductOrderService productOrderService;

        public OrderConsumer(IProductOrderService productOrderService)
            => this.productOrderService = productOrderService;

        public Task Consume(ConsumeContext<OrderMessage> context)
            => this.productOrderService.AddOrder(context.Message.Order);
    }
}