namespace Statistics.Services.Models
{
    using System.Collections.Generic;
    
    public class BoughtProductListOutputModel
    {
        public IEnumerable<BoughtProductOutputModel> Ids { get; set; }
    }
}