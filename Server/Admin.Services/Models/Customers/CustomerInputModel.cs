namespace Admin.Services.Models.Customers
{
    using StoreApi.Services.Contracts.Mapping;
    
    public class CustomerInputModel : IMapFrom<CustomerFormModel>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PhoneNumber { get; set; }
    }
}
