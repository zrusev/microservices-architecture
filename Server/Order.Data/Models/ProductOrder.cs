namespace Order.Data.Models
{
    public class ProductOrder
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
