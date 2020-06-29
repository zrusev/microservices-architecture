namespace Statistics.Services.Contracts
{
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    public interface IBoughtProductService
    {
        Task<QueryResult> GetBoughtProducts(int userId);
    }
}
