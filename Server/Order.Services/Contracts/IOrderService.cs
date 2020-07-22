namespace Order.Services.Contracts
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using System.Threading.Tasks;

    public interface IOrderService : IService
    {
        Task AddOrder(OrderInputModel model);
    }
}