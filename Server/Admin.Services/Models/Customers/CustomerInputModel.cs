namespace Admin.Services.Models.Customers
{
    using StoreApi.Services.Contracts.Mapping;
    
    public class CustomerInputModel : IMapFrom<CustomerFormModel>
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
