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

        Task<int> Total(string category, string manufacturer);

        Task SaveToDb(Product product);

        Task<IEnumerable<ProductOutputModel>> GetListings(int page, string category, string manufacturer);

        Task<QueryResult> GetDetails(string name);

        Task<IEnumerable<ProductOutputListModel>> GetDetails(int[] ids);

        Task<QueryResult> Create(ProductInputModel model);

        public IEnumerable<ProductServiceModel> Products();
    }
}
