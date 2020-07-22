namespace Customer.Services.Contracts
{
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Web.Messages;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductOrderService: IService
    {
        Task AddOrder(ICollection<ProductOrderMessage> model);
    }
}