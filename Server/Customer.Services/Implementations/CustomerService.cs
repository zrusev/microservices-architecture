namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Customer.Data;
    using Customer.Data.Models;
    using Microsoft.Extensions.Logging;
    using Models;
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    public class CustomerService: ICustomerService
    {
        private readonly ILogger<CustomerService> logger;
        private readonly IMapper mapper;
        private readonly CustomerDbContext db;

        public CustomerService(ILogger<CustomerService> logger,
            CustomerDbContext db,
            IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<QueryResult> CreateCustomer(CreateCustomerInputModel model, string userId, string email)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserId = userId,
                Email = email
            };

            this.db
                .Customers
                .Add(customer);

            await this.db.SaveChangesAsync();
            
            return QueryResult.Success;
        }
    }
}
