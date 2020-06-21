namespace Customer.Services
{
    using Customer.Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly CustomerDbContext db;

        public ProductService(CustomerDbContext db)
        {
            this.db = db;
        }

        IEnumerable<ProductServiceModel> IProductService.Products()
            => this.db
                .Products
                .Select(u => new ProductServiceModel
                {
                    Name = u.Name
                })
                .ToList();
    }
}
