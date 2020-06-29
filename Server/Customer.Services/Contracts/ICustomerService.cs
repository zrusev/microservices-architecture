namespace Customer.Services.Contracts
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    public interface ICustomerService : IService
    {
        Task<QueryResult> CreateCustomer(CustomerCreateInputModel model, string userId, string email);
        
        Task<QueryResult> GetById(string id);
    }
}
