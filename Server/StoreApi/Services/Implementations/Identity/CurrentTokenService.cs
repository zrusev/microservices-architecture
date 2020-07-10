namespace StoreApi.Services.Implementations.Identity
{
    using Contracts.Identity;
    
    public class CurrentTokenService : ICurrentTokenService
    {
        private string currentToken;

        public string Get() 
            => this.currentToken;

        public void Set(string token) 
            => this.currentToken = token;
    }
}