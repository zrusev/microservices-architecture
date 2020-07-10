namespace Admin.Services.Models.Customers
{
    using StoreApi.Services.Contracts.Mapping;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel : IMapFrom<CustomerDetailsOutputModel>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
