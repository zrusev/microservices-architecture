namespace Customer.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Common;
    using static Data.DataConstants.Customers;

    public class CustomerCreateInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string LastName { get; set; }

        [MaxLength(MaxAddressLength)]
        public string Address1 { get; set; }

        [MaxLength(MaxAddressLength)]
        public string Address2 { get; set; }

        [Required]
        [MinLength(MinPhoneNumberLength)]
        [MaxLength(MaxPhoneNumberLength)]
        [RegularExpression(PhoneNumberRegularExpression)]
        public string PhoneNumber { get; set; }
    }
}
