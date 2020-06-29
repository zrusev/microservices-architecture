namespace Customer.Services.Implementations
{
    using AutoMapper;
    using Contracts;
    using Customer.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models;
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

        public async Task<ManufacturerOutputModel> Find(int id)
            => await this.mapper
                .ProjectTo<ManufacturerOutputModel>(this.db
                    .Manufacturers
                    .Where(v => v.Id == id)
                    .Select(m => m))
               .FirstOrDefaultAsync();
    }
}