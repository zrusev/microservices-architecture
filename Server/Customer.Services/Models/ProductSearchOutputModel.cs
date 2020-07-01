namespace Customer.Services.Models
{
    using System.Collections.Generic;

    public class ProductSearchOutputModel
    {
        public IEnumerable<ProductOutputModel> Products { get; set; }

        public int Page { get; set; }

        public int TotalProducts { get; set; }
    }
}
