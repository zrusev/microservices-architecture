namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Customer.Data;
    using Customer.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ManufacturerService : IManufacturerService
    {
        private readonly ILogger<ManufacturerService> logger;
        private readonly IMapper mapper;
        private readonly CustomerDbContext db;

        public ManufacturerService(ILogger<ManufacturerService> logger,
            CustomerDbContext db,
            IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Manufacturer> Find(int manufacturerId)
            => await this.db
                .Manufacturers
                .Where(v => v.Id == manufacturerId)
                .Select(m => m)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<ManufacturerResultOutputModel>> Top()
            => await this.db
                    .Products
                    .GroupBy(p => p.Manufacturer.Name)
                    .Select(c => new ManufacturerResultOutputModel
                    {
                        Name = c.Key,
                        Products = c.Count()
                    })
                    .OrderByDescending(p => p.Products)
                    .Take(11)
                    .ToListAsync();
    }
}