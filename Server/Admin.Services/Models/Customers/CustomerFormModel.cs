namespace Admin.Services.Models.Customers
{
    using StoreApi.Services.Contracts.Mapping;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel : IMapFrom<CustomerDetailsOutputModel>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
