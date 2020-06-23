namespace Identity.Services.Contracts.User
{
    using Models.Facebook;
    using Models.User;
    using StoreApi.Services.Contracts.Services;
    using StoreApi.Services.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService: IScopedService
    {
        Task<QueryResult> Register(UserInputModel model);

        Task<QueryResult> Login(UserInputModel model);

        Task<QueryResult> LoginWithFacebook(FacebookAuthViewModel model);

        IEnumerable<UserServiceModel> Users();
    }
}