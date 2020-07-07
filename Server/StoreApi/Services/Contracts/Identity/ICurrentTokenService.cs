namespace StoreApi.Services.Contracts.Identity
{
    public interface ICurrentTokenService
    {
        string Get();

        void Set(string token);
    }
}
