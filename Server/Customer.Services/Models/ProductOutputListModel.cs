namespace Customer.Services.Models
{
    using Customer.Data.Models;
    using System.Collections.Generic;

    public class ProductOutputListModel
    {
        public IEnumerable<ProductOutputModel> Products { get; set; }

        public string Category { get; set; }
    }
}