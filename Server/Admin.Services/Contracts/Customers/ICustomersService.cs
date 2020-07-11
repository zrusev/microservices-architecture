namespace Admin.Services.Contracts.Customers
{
    using Models.Customers;
    using Refit;
    using StoreApi.Services.Contracts.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomersService : IService
    {
        [Get("/api/v1/Customers/All")]
        Task<IEnumerable<CustomerDetailsOutputModel>> All();

        [Get("/api/v1/Customers/{id}")]
        Task<CustomerDetailsOutputModel> Details(string id);

        [Put("/api/v1/Customers/{id}")]
        Task Edit(string id, CustomerInputModel Customer);
    }
}
