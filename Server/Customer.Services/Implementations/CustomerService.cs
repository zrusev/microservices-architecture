namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Customer.Data;
    using Customer.Services.Contracts;
    using Customer.Services.Models;
    using System.Threading.Tasks;
    using Customer.Data.Models;
    using StoreApi.Services.Helpers;

    public class CustomerService: ICustomerService
    {
        private readonly IMapper mapper;
        private readonly CustomerDbContext db;

        public CustomerService(CustomerDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<QueryResult> CreateCustomer(CreateCustomerInputModel model, string userId)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserId = userId
            };

            this.db
                .Customers
                .Add(customer);

            await this.db.SaveChangesAsync();
            
            return QueryResult.Success;
        }
    }
}
