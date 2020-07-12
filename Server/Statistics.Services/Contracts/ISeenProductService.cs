namespace Statistics.Services.Contracts
{
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    public interface ISeenProductService : IService
    {
        Task<QueryResult> GetTotalVisits(int productId);

        Task<QueryResult> AddVisits(int productId);
    }
}