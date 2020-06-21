namespace Identity.Services.Models.User
{
    using Data.Models.Users;
    using StoreApi.Mapping;

    public class UserServiceModel: IMapFrom<ApplicationUser>
    {
        public string Email { get; set; }
    }
}
