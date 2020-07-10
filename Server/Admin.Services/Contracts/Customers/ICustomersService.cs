namespace Admin.Services.Contracts.Customers
{
    using Models.Customers;
    using Refit;
    using StoreApi.Services.Contracts.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomersService : IService
    {
        [Get("/api/v1/Customers")]
        Task<IEnumerable<CustomerDetailsOutputModel>> All();

        [Get("/api/v1/Customers/{id}")]
        Task<CustomerDetailsOutputModel> Details(int id);

        [Put("/api/v1/Customers/{id}")]
        Task Edit(int id, CustomerInputModel Customer);
    }
}
