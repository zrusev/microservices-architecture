namespace Admin.Services.Models.Identity
{
    using StoreApi.Services.Contracts.Mapping;

    public class UserInputModel : IMapFrom<LoginFormModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
