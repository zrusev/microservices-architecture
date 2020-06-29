namespace Statistics.Services.Models
{
    using Statistics.Data.Models;
    using StoreApi.Services.Contracts.Mapping;

    public class BoughtProductOutputModel : IMapFrom<BoughtProduct>
    {
        public int ProductId { get; set; }
    }
}