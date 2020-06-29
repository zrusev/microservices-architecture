namespace Statistics.Services.Implementation
{
    using AutoMapper;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Statistics.Data;
    using Statistics.Services.Models;
    using StoreApi.Services.Helpers;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeenProductService : ISeenProductService
    {
        private readonly ILogger<SeenProductService> logger;
        private readonly StatisticsDbContext db;
        private readonly IMapper mapper;

        public SeenProductService(ILogger<SeenProductService> logger,
            StatisticsDbContext db,
            IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<QueryResult> GetTotalVisits(int productId)
            => QueryResult<SeenProductOutputModel>.Suceeded(
                await this.mapper
                .ProjectTo<SeenProductOutputModel>(this.db
                    .SeenProducts
                    .Where(v => v.ProductId == productId)
                    .Select(v => v.TotalVisits))
                .FirstOrDefaultAsync());
    }
}
