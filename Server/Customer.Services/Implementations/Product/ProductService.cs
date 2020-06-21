namespace Customer.Services
{
    using Customer.Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly CustomerDbContext _db;

        public ProductService(CustomerDbContext db)
        {
            _db = db;
        }

        IEnumerable<ProductServiceModel> IProductService.Products()
            => _db
                .Products
                .Select(u => new ProductServiceModel
                {
                    Name = u.Name
                })
                .ToList();
    }
}
