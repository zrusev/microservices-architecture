namespace Customer.Gateway.Services.Models
{
    using System.Collections.Generic;

    public class TopBoughtProductOutputModel
    {
        public IEnumerable<BoughtProductOutputModel> Ids { get; set; }
    }
}
