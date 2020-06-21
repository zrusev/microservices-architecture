namespace Identity.Services.Implementations.User
{
    using Contracts.User;
    using Data;
    using Models.User;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserServiceModel> Users()
            => this.db
                .Users
                .Select(u => new UserServiceModel
                {
                    Email = u.Email
                })
                .ToList();
    }
}
