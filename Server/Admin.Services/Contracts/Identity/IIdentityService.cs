namespace Admin.Services.Contracts.Identity
{
    using System.Threading.Tasks;
    using Models.Identity;
    using Refit;
    using StoreApi.Services.Contracts.Services;

    public interface IIdentityService : IService
    {
        [Post("/api/v1/Users/Login")]
        Task<UserOutputModel> Login([Body] UserInputModel loginInput);
    }
}
