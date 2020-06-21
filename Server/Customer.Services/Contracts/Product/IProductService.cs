namespace Customer.Services
{
    using StoreApi.Services;
    using Models;
    using System.Collections.Generic;

    public interface IProductService: IScopedService
    {
        public IEnumerable<ProductServiceModel> Products();
    }
}
