﻿namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Customer.Data;
    using Customer.Data.Models;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models;
    using StoreApi.Services.Helpers;
    using StoreApi.Services.Infrastructure;
    using StoreApi.Web.Messages;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private const int ProductsPerPage = 10;

        private readonly ILogger<ProductService> logger;
        private readonly IMapper mapper;
        private readonly IBus publisher;
        private readonly CustomerDbContext db;

        public ProductService(ILogger<ProductService> logger,
            CustomerDbContext db,
            IMapper mapper,
            IBus publisher)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task<int> Total(string category, string manufacturer, string name)
            => await this.db
                .Products
                .WhereIf(!string.IsNullOrWhiteSpace(category), 
                    c => c.Category.Name.ToLower() == category.ToLower())
                .WhereIf(!string.IsNullOrWhiteSpace(manufacturer), 
                    m => m.Manufacturer.Name.ToLower() == manufacturer.ToLower())
                .WhereIf(!string.IsNullOrWhiteSpace(name),
                    n => n.Name.ToLower().Contains(name.ToLower()))
                .Select(v => v.Id)
                .CountAsync();

        public async Task<IEnumerable<ProductOutputModel>> GetListings(int page, string category, string manufacturer, string name)
            => await this.mapper
                .ProjectTo<ProductOutputModel>(this.db
                    .Products
                    .Include(c => c.Category)
                    .Include(m => m.Manufacturer)
                    .WhereIf(!string.IsNullOrWhiteSpace(category), 
                        c => c.Category.Name == category.ToLower())
                    .WhereIf(!string.IsNullOrWhiteSpace(manufacturer), 
                        m => m.Manufacturer.Name == manufacturer.ToLower())
                    .WhereIf(!string.IsNullOrWhiteSpace(name),
                        n => n.Name.ToLower().Contains(name.ToLower()))
                    .Select(p => p)
                    .Skip((page - 1) * ProductsPerPage)
                    .Take(ProductsPerPage))
                .ToListAsync();

        public async Task<QueryResult> GetDetails(int id, string name)
        {
            var result = await this.mapper
                    .ProjectTo<ProductOutputModel>(this.db
                        .Products
                        .Include(c => c.Category)
                        .Include(m => m.Manufacturer)
                        .Where(p => p.Id == id && p.Name.ToLower() == name.ToLower())
                        .Select(p => p))
                    .FirstOrDefaultAsync();

            if (result == null)
            {
                return QueryResult.Failed(Errors.Log("MissingProduct", "This product is missing"));
            }

            await this.publisher.Publish(new SeenProductMessage
            {
                Id = result.Id
            });

            return QueryResult<ProductOutputModel>.Suceeded(result);
        }

        public async Task<IEnumerable<ProductOutputListModel>> GetDetails(int[] ids)
        {
            var products = await this.mapper
                    .ProjectTo<ProductOutputModel>(this.db
                        .Products
                        .Include(c => c.Category)
                        .Include(m => m.Manufacturer)
                        .Where(p => ids.Contains(p.Id))
                        .Select(p => p))
                    .ToListAsync();

            var result = products
                .GroupBy(c => c.Category)
                .Where(c => c.Count() >= 4)
                .Select(x => new ProductOutputListModel
                {
                    Category = x.Key,
                    Products = x.Select(p => new ProductOutputModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Category = p.Category,
                        Manufacturer = p.Manufacturer,
                        Image_url = p.Image_url
                    }).Take(4)
                })
                .ToList();

            return result;
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

        public async Task SaveToDb(Product product)
        {
            this.db
                  .Products
                  .Update(product);

            await this.db
                .SaveChangesAsync();
        }

        IEnumerable<ProductServiceModel> IProductService.Products()
            => this.db
                .Products
                .Select(u => new ProductServiceModel
                {
                    Name = u.Name
                })
                .ToList();

        public async Task<Product> Find(int id)
            => await this.db
                .Products
                .Include(c => c.Category)
                .Include(m => m.Manufacturer)
                .Where(p => p.Id == id)
                .Select(p => p)
                .FirstOrDefaultAsync();
    }
}
