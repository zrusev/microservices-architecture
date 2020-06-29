namespace Customer.Services.Models
{
    using Customer.Data.Models;
    using StoreApi.Services.Contracts.Mapping;

    public class ManufacturerOutputModel : IMapFrom<Manufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}