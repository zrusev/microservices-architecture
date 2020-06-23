namespace StoreApi.Services.Contracts.Mapping
{
    using AutoMapper;

    public interface IMapExplicitly
    {
        public void RegisterMappings(IProfileExpression profile);
    }
}
