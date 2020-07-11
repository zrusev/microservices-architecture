namespace StoreApi.Services.Contracts.Identity
{
    using Services;
    
    public interface ICurrentTokenService: IScopedService
    {
        string Get();

        void Set(string token);
    }
}
