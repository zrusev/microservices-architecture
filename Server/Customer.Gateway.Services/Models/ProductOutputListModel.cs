namespace Customer.Gateway.Services.Models
{
    using System.Collections.Generic;

    public class ProductOutputListModel
    {
        public IEnumerable<ProductOutputModel> Products { get; set; }

        public string Category { get; set; }
    }
}