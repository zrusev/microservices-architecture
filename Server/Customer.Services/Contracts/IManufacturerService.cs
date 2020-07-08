namespace Customer.Services.Contracts
{
    using Data.Models;
    using Models;
    using StoreApi.Services.Contracts.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IManufacturerService : IService
    {
        Task<Manufacturer> Find(int manufacturerId);

        Task<IEnumerable<ManufacturerResultOutputModel>> Top();
    }
}