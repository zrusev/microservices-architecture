namespace Order.Services.Implementation
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Contracts;
    using StoreApi.Data.Models;
    using StoreApi.Services.Implementations.Data;
    using StoreApi.Web.Messages;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class OrderService : DataService<Order>, IOrderService
    {
        private readonly ILogger<OrderService> logger;
        private readonly OrderDbContext db;
        private readonly IMapper mapper;
        private readonly IBus publisher;

        public OrderService(ILogger<OrderService> logger,
            OrderDbContext db,
            IMapper mapper,
            IBus publisher)
                :base(db)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task AddOrder(OrderInputModel model)
        {
            Order orderModel = new Order
            {
                OrderDate = model.OrderDate,
                ShippedDate = model.ShippedDate,
                Status = StatusEnum.Pending,
                Products = this.mapper.Map<ICollection<ProductOrderInputModel>, ICollection<ProductOrder>>(model.Products)
            };

            await this.db
                .Orders
                .AddAsync(orderModel);

            OrderMessage orderMessage = new OrderMessage
            {
                Order = new List<ProductOrderMessage>()
            };

            foreach (var product in orderModel.Products)
            {
                orderMessage.Order.Add(new ProductOrderMessage
                {
                    ProductId = product.ProductId,
                    ItemId = product.ItemId,
                    OrderId = orderModel.Id,
                    Quantity = product.Quantity
                });
            }

            Message message = new Message(orderMessage);

            await this.AddEntity(message);
            
            await this.publisher.Publish(orderMessage);
            await this.MarkMessageAsPublished(message);

            this.logger.LogInformation(
                $"Order with Id: {orderModel.Id} has been successfully created.");

            await Task.CompletedTask;
        }
    }
}