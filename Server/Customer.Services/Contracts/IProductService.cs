namespace Customer.Services.Contracts
{
    using Customer.Data.Models;
    using Models;
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService: IService
    {
        Task<Product> Find(int id);

        Task<bool> Delete(int id);

        Task<int> Total();

        Task SaveToDb(Product product);

        Task<IEnumerable<ProductOutputModel>> GetListings(int page);

        Task<QueryResult> GetDetails(int id);

        Task<QueryResult> Create(ProductInputModel model);

        public IEnumerable<ProductServiceModel> Products();
    }
}
