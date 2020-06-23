namespace Identity.Services.Models.User
{
    using System.ComponentModel.DataAnnotations;
    
    public class UserInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
