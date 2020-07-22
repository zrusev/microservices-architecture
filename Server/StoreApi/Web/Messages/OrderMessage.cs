namespace StoreApi.Web.Messages
{
    using System.Collections.Generic;

    public class OrderMessage
    {
        public OrderMessage()
        {
            this.Order = new List<ProductOrderMessage>();
        }
        public ICollection<ProductOrderMessage> Order { get; set; }
    }
}
