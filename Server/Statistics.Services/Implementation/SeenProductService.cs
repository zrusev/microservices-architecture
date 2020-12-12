namespace Statistics.Services.Implementation
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Statistics.Data;
    using Statistics.Services.Models;
    using StoreApi.Services.Contracts.Data;
    using StoreApi.Services.Helpers;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServiceConstants.MemoryDatabaseKeys;

    public class SeenProductService : ISeenProductService
    {
        private readonly ILogger<SeenProductService> logger;
        private readonly IMemoryDatabase memoryDatabase;
        private readonly StatisticsDbContext db;
        private readonly IMapper mapper;

        public SeenProductService(ILogger<SeenProductService> logger,
            IMemoryDatabase memoryDatabase,
            StatisticsDbContext db,
            IMapper mapper)
        {
            this.logger = logger;
            this.memoryDatabase = memoryDatabase;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<QueryResult> GetTotalVisits(int productId)
        {
            var product = await this.db
                    .SeenProducts
                    .Where(v => v.ProductId == productId)
                    .Select(v => v)
                    .FirstOrDefaultAsync();

            if (product == null)
            {
                product = new SeenProduct()
                {
                    ProductId = productId,
                    TotalVisits = 1
                };
            }

            return QueryResult<SeenProductOutputModel>.Suceeded(
                this.mapper.Map<SeenProductOutputModel>(product));
        }

        public async Task<QueryResult> AddVisits(int productId)
        {
            //var product = await this.db
            //    .SeenProducts
            //    .Where(v => v.ProductId == productId)
            //    .Select(v => v)
            //    .FirstOrDefaultAsync();

            //if (product == null)
            //{
            //    product = new SeenProduct();
            //    product.ProductId = productId;
            //}

            //product.TotalVisits += 1;

            //this.db.SeenProducts.Update(product);
            //this.db.SaveChanges();

            //return QueryResult<SeenProductOutputModel>.Suceeded(
            //    this.mapper.Map<SeenProductOutputModel>(product));
            
            await this.memoryDatabase.Increment(StatisticsTotalVisitsKey);

            return QueryResult<SeenProductOutputModel>.Suceeded(
                this.mapper.Map<SeenProductOutputModel>(null));
        }
    }
}
