namespace Customer.Services
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using System.Collections.Generic;

    public interface IProductService: IScopedService
    {
        public IEnumerable<ProductServiceModel> Products();
    }
}
