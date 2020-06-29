namespace Customer.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Customer.Data.DataConstants.Products;
    
    public class ProductInputModel
    {
        [Required]
        [MinLength(MinModelLength)]
        [MaxLength(MaxModelLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(MinModelLength)]
        [MaxLength(MaxModelLength)]
        public string Description { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        [Url]
        public string Image_url { get; set; }

        public int CategoryId { get; set; }

        public int ManufacturerId { get; set; }
    }
}