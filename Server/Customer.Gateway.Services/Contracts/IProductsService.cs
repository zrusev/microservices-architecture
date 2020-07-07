namespace Customer.Gateway.Services.Contracts
{
    using Models;
    using Refit;
    using StoreApi.Services.Contracts.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService : IService
    {
        [Get("/api/v1/Products/Details")]
        Task<IEnumerable<ProductOutputListModel>> GetProducts(
            [Query(CollectionFormat.Multi)] IEnumerable<int> ids);
    }
}