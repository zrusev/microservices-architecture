namespace StoreApi.Services.Contracts.Data
{
    using StoreApi.Data.Models;
    using System.Threading.Tasks;
    
    public interface IDataService<in TEntity>
        where TEntity : class
    {
        Task AddEntity(TEntity entity);

        Task AddEntity(params object[] entities);

        Task UpdateEntity(TEntity entity);

        Task UpdateEntity(params object[] entities);

        Task RemoveEntity(TEntity entity);

        Task RemoveEntity(params object[] entities);

        Task MarkMessageAsPublished(Message entity);
    }
}
