namespace Customer.Services.Models
{
    using AutoMapper;
    using Data.Models.Products;
    using StoreApi.Services.Contracts.Mapping;

    public class ProductServiceModel: IMapFrom<Product>, IMapExplicitly
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void RegisterMappings(IProfileExpression profile)
        {
            profile
                .CreateMap<Product, ProductServiceModel>()
                .ForMember(a => a.Name, cfg => cfg.MapFrom(a => a.Name));
        }
    }
}
