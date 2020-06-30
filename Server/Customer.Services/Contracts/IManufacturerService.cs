namespace Customer.Services.Contracts
{
    using Customer.Data.Models;
    using StoreApi.Services.Contracts.Services;
    using System.Threading.Tasks;

    public interface IManufacturerService : IService
    {
        Task<Manufacturer> Find(int manufacturerId);
    }
}