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

    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> logger;
        private readonly IMapper mapper;
        private readonly CustomerDbContext db;

        public CategoryService(ILogger<CategoryService> logger,
            CustomerDbContext db,
            IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Category> Find(int categoryId)
            => await this.db
                .Categories
                .Where(v => v.Id == categoryId)
                .Select(c => c)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<CategoryOutputModel>> GetAll()
            => await this.mapper
                .ProjectTo<CategoryOutputModel>(this.db
                    .Categories
                    .Select(c => c))
                .ToListAsync();
    }
}
