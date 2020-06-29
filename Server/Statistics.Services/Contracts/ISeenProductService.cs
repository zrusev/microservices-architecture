namespace Statistics.Services.Contracts
{
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    public interface ISeenProductService
    {
        Task<QueryResult> GetTotalVisits(int productId);
    }
}