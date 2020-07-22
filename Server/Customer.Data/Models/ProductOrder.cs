﻿namespace Customer.Data.Models
{
    public class ProductOrder
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int OrderId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}