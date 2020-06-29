namespace Customer.Services.Contracts
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService: IService
    {
        Task<ProductOutputModel> Find(int id);

        Task<bool> Delete(int id);

        Task<int> Total();

        Task SaveToDb();

        Task<IEnumerable<ProductOutputModel>> GetListings(int page);

        Task<QueryResult> GetDetails(int id);

        Task<QueryResult> Create(ProductInputModel model);

        public IEnumerable<ProductServiceModel> Products();
    }
}
