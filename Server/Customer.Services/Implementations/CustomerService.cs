namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Customer.Data;
    using Customer.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models;
    using StoreApi.Services.Helpers;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<QueryResult> CreateCustomer(CustomerCreateInputModel model, string userId, string email)
        {
            var exists = await this.db
                    .Customers
                    .Where(v => v.UserId == userId)
                    .AnyAsync();

            if (exists)
            {
                this.logger.LogInformation($"Customer with UserID: {userId} has already been created.");

                return QueryResult.Failed(Errors.Log("ExistingUser", "Customer has already been created."));
            }

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

            return QueryResult<Customer>.Suceeded(customer);
        }

        public async Task<QueryResult> EditCustomer(string userId, EditCustomerInputModel model)
        {
            var customer = await this.db
                .Customers
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                this.logger.LogInformation($"Editing to Customer with UserID: {userId} was not found.");

                return QueryResult.Failed(Errors.Log("MissingCustomer", "Customer was not found."));
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Address1 = model.Address1;
            customer.Address2 = model.Address2;
            customer.PhoneNumber = model.PhoneNumber;

            this.db.Customers.Update(customer);
            this.db.SaveChanges();

            return QueryResult<Customer>.Suceeded(customer);
        }

        public async Task<QueryResult> GetById(string id)
        {
            var customer = await this.mapper
                    .ProjectTo<CustomerOutputModel>(this.db
                        .Customers
                        .Where(v => v.UserId == id)
                        .Select(u => u))
                    .FirstOrDefaultAsync();

            if (customer == null)
            {
                this.logger.LogInformation("Invalid User ID");

                return QueryResult.Failed(Errors.Log("InvalidUserId", "Invalid User ID"));
            }

            return QueryResult<CustomerOutputModel>.Suceeded(customer);
        }

        public async Task<QueryResult> GetAllCustomers()
        { 
            var customers = await this.mapper
                .ProjectTo<CustomerOutputModel>(this.db
                .Customers
                .Select(c => c))
                .ToListAsync();

            return QueryResult<IEnumerable<CustomerOutputModel>>.Suceeded(customers);
        } 
    }
}
