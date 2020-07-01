namespace Customer.Services.Models
{
    using Customer.Data.Models;
    using StoreApi.Services.Contracts.Mapping;

    public class CustomerOutputModel : IMapFrom<Customer>
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }
    }
}