namespace Customer.Data.Models
{
    using System.Collections.Generic;
    
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
