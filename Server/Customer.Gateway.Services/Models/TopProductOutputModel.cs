namespace Customer.Gateway.Services.Models
{
    using System.Collections.Generic;

    public class TopProductOutputModel
    {
        public IEnumerable<ProductOutputModel> TopProducts { get; set; }
    }
}