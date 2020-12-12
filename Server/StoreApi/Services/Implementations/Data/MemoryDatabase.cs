namespace StoreApi.Services.Implementations.Data
{
    using Contracts.Data;
    using StackExchange.Redis;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MemoryDatabase : IMemoryDatabase
    {
        private readonly IConnectionMultiplexer connection;

        public MemoryDatabase(IConnectionMultiplexer connection)
            => this.connection = connection;

        public Task<TValue> Get<TValue>(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> Increment(string key)
        {
            var database = this.connection.GetDatabase();

            return database.StringIncrementAsync(key);
        }

        public Task<double> IncrementSortedSet(string sortedSetKey, int value)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TValue>> RangeSortedSet<TValue>(string sortedSetKey, int start, int end)
        {
            throw new System.NotImplementedException();
        }

        public Task SetHash(string hashKey, IDictionary<string, object> values)
        {
            throw new System.NotImplementedException();
        }
    }
}
