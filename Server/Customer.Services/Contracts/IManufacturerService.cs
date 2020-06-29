namespace Customer.Services.Contracts
{
    using Customer.Services.Models;
    using System.Threading.Tasks;

    public interface IManufacturerService
    {
        Task<ManufacturerOutputModel> Find(int id);
    }
}