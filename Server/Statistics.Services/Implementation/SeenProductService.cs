namespace Statistics.Services.Implementation
{
    using Contracts;
    using Microsoft.Extensions.Logging;
    using Statistics.Services.Models;
    using StoreApi.Services.Contracts.Data;
    using StoreApi.Services.Helpers;
    using System.Threading.Tasks;

    using static ServiceConstants.MemoryDatabaseKeys;

    public class SeenProductService : ISeenProductService
    {
        private readonly ILogger<SeenProductService> logger;
        private readonly IMemoryDatabase memoryDatabase;

        public SeenProductService(ILogger<SeenProductService> logger,
            IMemoryDatabase memoryDatabase)
        {
            this.logger = logger;
            this.memoryDatabase = memoryDatabase;
        }

        public async Task<QueryResult> GetTotalVisits(int productId)
        {
            var visits = await this.memoryDatabase.GetFromSortedSet(StatisticsProductVisitsKey, productId);
            
            return QueryResult<SeenProductOutputModel>.Suceeded(
                new SeenProductOutputModel
                {
                    TotalVisits = visits.HasValue ? (int)visits : 1
                });
        }

        public async Task<QueryResult> AddVisits(int productId)
        {
            var visits = await this.memoryDatabase.GetFromSortedSet(StatisticsProductVisitsKey, productId);

            await this.memoryDatabase.IncrementSortedSet(StatisticsProductVisitsKey, productId);

            return QueryResult<SeenProductOutputModel>.Suceeded(
                new SeenProductOutputModel
                {
                    TotalVisits = visits.HasValue ? (int)visits : 1
                });
        }
    }
}