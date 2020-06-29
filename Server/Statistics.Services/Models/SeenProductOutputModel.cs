namespace Statistics.Services.Models
{
    using Statistics.Data.Models;
    using StoreApi.Services.Contracts.Mapping;

    public class SeenProductOutputModel : IMapFrom<SeenProduct>
    {
        public int TotalVisits { get; set; }
    }
}
