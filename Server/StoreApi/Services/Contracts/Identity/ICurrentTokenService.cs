namespace StoreApi.Services.Contracts.Identity
{
    using Services;
    
    public interface ICurrentTokenService: IService
    {
        string Get();

        void Set(string token);
    }
}
