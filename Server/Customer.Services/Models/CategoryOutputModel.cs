namespace Customer.Services.Models
{
    using Customer.Data.Models;
    using StoreApi.Services.Contracts.Mapping;

    public class CategoryOutputModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}