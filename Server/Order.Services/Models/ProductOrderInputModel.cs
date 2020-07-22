namespace Order.Services.Models
{
    using Order.Data.Models;
    using StoreApi.Services.Contracts.Mapping;

    public class ProductOrderInputModel : IMapTo<ProductOrder>
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
