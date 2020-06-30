namespace Customer.Services.Contracts
{
    using Customer.Data.Models;
    using Models;
    using StoreApi.Services.Contracts.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService : IService
    {
        Task<Category> Find(int categoryId);

        Task<IEnumerable<CategoryOutputModel>> GetAll();
    }
}
