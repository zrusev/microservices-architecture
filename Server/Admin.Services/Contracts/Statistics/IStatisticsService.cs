namespace Admin.Services.Contracts.Statistics
{
    using Models.Statistics;
    using Refit;
    using StoreApi.Services.Contracts.Services;
    using System.Threading.Tasks;

    public interface IStatisticsService : IService
    {
        [Get("/api/v1/BoughtProducts/TotalBoughtProducts")]
        Task<StatisticsOutputModel> BoughtProducts();
    }
}