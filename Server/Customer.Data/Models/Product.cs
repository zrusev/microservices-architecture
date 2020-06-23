namespace Customer.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Url { get; set; }

        public string Image_url { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public int ManufacturerId { get; set; }
    }
}
