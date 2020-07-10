namespace Customer.Gateway.Services.Contracts
{
    using Models;
    using Refit;
    using StoreApi.Services.Contracts.Services;
    using System.Threading.Tasks;

    public interface ITopBoughtProductsService : IService
    {
        [Get("/api/v1/BoughtProducts/TopBoughtProducts")]
        Task<TopBoughtProductOutputModel> TopBoughtProducts();
    }
}