namespace Customer.Services.Models
{
    using AutoMapper;
    using Customer.Data.Models;
    using StoreApi.Services.Contracts.Mapping;

    public class ProductOutputModel : IMapFrom<Product>, IMapExplicitly
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Url { get; set; }

        public string Image_url { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public int ManufacturerId { get; set; }

        public string Manufacturer { get; set; }

        public void RegisterMappings(IProfileExpression mapper)
            => mapper
                .CreateMap<Product, ProductOutputModel>()
                .ForMember(pr => pr.Manufacturer, cfg => cfg.MapFrom(p => p.Manufacturer.Name))
                .ForMember(pr => pr.Category, cfg => cfg.MapFrom(p => p.Category.Name));
    }
}
