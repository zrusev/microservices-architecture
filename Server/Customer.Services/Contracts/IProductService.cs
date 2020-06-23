namespace Customer.Services.Contracts
{
    using Models;
    using StoreApi.Services.Contracts.Services;
    using System.Collections.Generic;

    public interface IProductService: IService
    {
        public IEnumerable<ProductServiceModel> Products();
    }
}
