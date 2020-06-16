namespace StoreApi.Services.Models.User
{
    using Data.Models.Users;
    using StoreApi.Common.Mapping;

    public class UserServiceModel: IMapFrom<ApplicationUser>
    {
        public string Email { get; set; }
    }
}
