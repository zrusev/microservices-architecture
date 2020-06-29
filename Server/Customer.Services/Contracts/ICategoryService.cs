namespace Customer.Services.Contracts
{
    using Customer.Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<Category> Find(int categoryId);

        Task<IEnumerable<CategoryOutputModel>> GetAll();
    }
}
