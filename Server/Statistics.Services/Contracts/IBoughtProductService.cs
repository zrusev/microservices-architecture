namespace Statistics.Services.Contracts
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBoughtProductService : IService
    {
        Task<QueryResult> GetBoughtProducts(int userId);

        Task<QueryResult> TotalBoughtProducts();

        Task<IEnumerable<BoughtProductOutputModel>> TopBoughtProducts();
    }
}
