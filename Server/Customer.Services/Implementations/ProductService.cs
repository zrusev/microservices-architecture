namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Customer.Data;
    using Customer.Data.Models;
    using Customer.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using StoreApi.Services.Helpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private const int ProductsPerPage = 10;

        private readonly ILogger<ProductService> logger;
        private readonly IMapper mapper;
        private readonly CustomerDbContext db;

        public ProductService(ILogger<ProductService> logger,
            CustomerDbContext db,
            IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<int> Total()
            => await this.db
                .Products
                .Select(v => v.Id)
                .CountAsync();

        public async Task<IEnumerable<ProductOutputModel>> GetListings(int page)
            => await this.mapper
                .ProjectTo<ProductOutputModel>(this.db
                    .Products
                    .Select(p => p)
                    .Skip((page - 1) * ProductsPerPage)
                    .Take(ProductsPerPage))
                .ToListAsync();

        public async Task<QueryResult> GetDetails(int id)
        {
            var result = await this.mapper
                    .ProjectTo<ProductOutputModel>(this.db
                        .Products
                        .Where(p => p.Id == id)
                        .Select(p => p))
                    .FirstOrDefaultAsync();

            if (result == null)
            {
                return QueryResult.Failed(Errors.Log("MissingProduct", "This product is missing"));
            }

            return QueryResult<ProductOutputModel>.Suceeded(result);
        }

        public async Task<QueryResult> Create(ProductInputModel model)
        {
            this.db
                .Products
                .Add(new Product
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        Url = model.Url,
                        Image_url = model.Image_url,
                        CategoryId = model.CategoryId,
                        ManufacturerId = model.ManufacturerId
                    });

            await this.db
                .SaveChangesAsync();

            return QueryResult.Success;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await this.db
                .FindAsync<Product>(id);

            if (product == null)
            {
                return false;
            }

            this.db
                .Remove(product);

            await this.db
                .SaveChangesAsync();

            return true;
        }

        public async Task SaveToDb()
            => await this.db
                .SaveChangesAsync();

        IEnumerable<ProductServiceModel> IProductService.Products()
            => this.db
                .Products
                .Select(u => new ProductServiceModel
                {
                    Name = u.Name
                })
                .ToList();

        public async Task<ProductOutputModel> Find(int id)
            => await this.mapper
                .ProjectTo<ProductOutputModel>(this.db
                    .Products
                    .Include(c => c.Category)
                    .Include(m => m.Manufacturer)
                    .Where(p => p.Id == id)
                    .Select(p => p))
              .FirstOrDefaultAsync();
    }
}
