namespace Statistics.Services.Implementation
{
    using AutoMapper;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models;
    using Statistics.Data;
    using StoreApi.Services.Helpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BoughtProductService : IBoughtProductService
    {
        private readonly ILogger<SeenProductService> logger;
        private readonly StatisticsDbContext db;
        private readonly IMapper mapper;

        public BoughtProductService(ILogger<SeenProductService> logger,
            StatisticsDbContext db,
            IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<QueryResult> GetBoughtProducts(int userId)
            => QueryResult<IEnumerable<BoughtProductOutputModel>>.Suceeded(
                await this.mapper
                    .ProjectTo<BoughtProductOutputModel>(this.db
                        .BoughtProducts
                        .Where(v => v.UserId == userId))
                    .ToListAsync());

        public async Task<IEnumerable<BoughtProductOutputModel>> TopBoughtProducts()
            => await this.db
                    .BoughtProducts
                    .GroupBy(p => p.ProductId)
                    .Select(c => new BoughtProductOutputModel
                    {
                        ProductId = c.Key,
                        Count = c.Count()
                    })
                    .OrderBy(p => p.Count)
                    .Take(100)
                    .ToListAsync();

        public async Task<QueryResult> TotalBoughtProducts()
            => QueryResult<BoughtProductsTotalOutputModel>.Suceeded(
                await this.db
                        .BoughtProducts
                        .GroupBy(p => p.Id)
                        .Select(m => new BoughtProductsTotalOutputModel
                        {
                            Total = m.Key
                        })
                        .FirstOrDefaultAsync());
    }
}