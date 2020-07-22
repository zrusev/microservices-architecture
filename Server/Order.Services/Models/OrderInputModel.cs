namespace Order.Services.Models
{
    using Data.Models;
    using StoreApi.Services.Contracts.Mapping;
    using System;
    using System.Collections.Generic;

    public class OrderInputModel : IMapFrom<Order>
    {
        public OrderInputModel()
        {
            this.Products = new List<ProductOrderInputModel>();
        }

        public byte Status { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public ICollection<ProductOrderInputModel> Products { get; set; }
    }
}