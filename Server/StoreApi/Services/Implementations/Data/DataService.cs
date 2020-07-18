namespace StoreApi.Services.Implementations.Data
{
    using Contracts.Data;
    using Microsoft.EntityFrameworkCore;
    using StoreApi.Data.Models;
    using System.Threading.Tasks;

    public abstract class DataService<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        protected DataService(DbContext db) => this.Data = db;

        protected DbContext Data { get; }

        public async Task AddEntity(TEntity entity)
        {
            await this.Data
                    .AddAsync(entity);

            await this.Data
                .SaveChangesAsync();
        }

        public async Task AddEntity(params object[] entities)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                await this.Data
                    .AddAsync(entities[i]);
            }

            await this.Data
                .SaveChangesAsync();
        }

        public async Task UpdateEntity(TEntity entity)
        {
            this.Data
                .Update(entity);

            await this.Data
                .SaveChangesAsync();
        }

        public async Task UpdateEntity(params object[] entities)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                this.Data
                    .Update(entities[i]);
            }

            await this.Data
                .SaveChangesAsync();
        }

        public async Task MarkMessageAsPublished(Message entity)
        {
            var message = await this.Data
                .FindAsync<Message>(entity.Id);

            message.MarkAsPublished();

            await this.Data
                .SaveChangesAsync();
        }

        public async Task RemoveEntity(params object[] entities)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                this.Data
                    .Remove(entities[i]);
            }

            await this.Data
                .SaveChangesAsync();
        }

        public async Task RemoveEntity(TEntity entity)
        {
            this.Data
                .Remove(entity);

            await this.Data
                .SaveChangesAsync();
        }
    }
}
