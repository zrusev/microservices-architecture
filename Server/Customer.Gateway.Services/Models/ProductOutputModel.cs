namespace Customer.Gateway.Services.Models
{
    public class ProductOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Url { get; set; }

        public string Image_url { get; set; }

        public string Category { get; set; }

        public string Manufacturer { get; set; }
    }
}
