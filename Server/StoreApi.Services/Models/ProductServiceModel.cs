namespace StoreApi.Services.Models
{
    using AutoMapper;
    using StoreApi.Common.Mapping;
    using StoreApi.Data.Models.Products;

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
