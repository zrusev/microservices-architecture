namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.Extensions.Logging;
    using StoreApi.Web.Messages;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductOrderService : IProductOrderService
    {
        private readonly ILogger<ProductOrderService> logger;
        private readonly IMapper mapper;
        private readonly CustomerDbContext db;

        public ProductOrderService(ILogger<ProductOrderService> logger,
            IMapper mapper,
            CustomerDbContext db)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddOrder(ICollection<ProductOrderMessage> model)
        {
            List<ProductOrder> products = new List<ProductOrder>();

            foreach (var product in model)
            {
                products.Add(new ProductOrder
                {
                    OrderId = product.OrderId,
                    ItemId = product.ItemId,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    // Product = await this.db.Products.Where(p => p.Id == product.ProductId).Select(p => p).FirstOrDefaultAsync()
                });
            }

            await this.db
                .ProductOrders
                .AddRangeAsync(products);

            await this.db
                .SaveChangesAsync();
        }
    }
}