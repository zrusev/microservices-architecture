namespace StoreApi.Web.Messages
{
    public class ProductOrderMessage
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
