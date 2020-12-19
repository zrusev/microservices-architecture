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

        public Task<long> Increment(string key)
        {
            var database = this.connection.GetDatabase();

            return database.StringIncrementAsync(key);
        }

        public Task<double> IncrementSortedSet(string sortedSetKey, int value)
        {
            var database = this.connection.GetDatabase();

            return database.SortedSetIncrementAsync(sortedSetKey, value, 1);
        }

        public Task<double?> GetFromSortedSet(string sortedSetKey, int value)
        {
            var database = this.connection.GetDatabase();

            return database.SortedSetScoreAsync(sortedSetKey, value);
        }
             
        public Task<IEnumerable<TValue>> RangeSortedSet<TValue>(string sortedSetKey, int start, int end)
        {
            throw new System.NotImplementedException();
        }
    }
}
