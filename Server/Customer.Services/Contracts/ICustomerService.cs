namespace Customer.Services.Contracts
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    public interface ICustomerService : IService
    {
        Task<QueryResult> CreateCustomer(CustomerCreateInputModel model, string userId, string email);

        Task<QueryResult> EditCustomer(string id, EditCustomerInputModel model);
        
        Task<QueryResult> GetById(string id);

        Task<QueryResult> GetAllCustomers();
    }
}
