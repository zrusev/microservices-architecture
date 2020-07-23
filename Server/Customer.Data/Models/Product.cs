namespace Customer.Data.Models
{
    using System.Collections.Generic;
    
    public class Product
    {
        public Product()
        {
            this.Orders = new List<ProductOrder>();
        }

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

        public ICollection<ProductOrder> Orders { get; set; }
    }
}
