namespace Order.Services.Contracts
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    public interface IOrderService : IService
    {
        Task<QueryResult> AddOrder(OrderInputModel model);
    }
}