namespace Identity.Services
{
    using Models.User;
    using StoreApi.Services;
    using System.Collections.Generic;

    public interface IUserService: IScopedService
    {
        public IEnumerable<UserServiceModel> Users();
    }
}
