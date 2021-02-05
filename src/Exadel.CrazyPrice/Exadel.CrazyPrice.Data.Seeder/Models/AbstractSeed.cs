using Exadel.CrazyPrice.Data.Seeder.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models
{
    public abstract class AbstractSeed<TCollection> : IDisposable, ISeed where TCollection : class
    {
        private readonly bool _clearDbBeforeSeed;
        private readonly bool _rewriteIndexes;
        private Timer _timer;

        protected uint DefaultCountSeed;
        protected IMongoCollection<TCollection> Collection;
        protected List<CreateIndexModel<TCollection>> IndexModels;

        protected AbstractSeed(SeederConfiguration configuration)
        {
            _clearDbBeforeSeed = configuration.ClearDatabaseBeforeSeed;
            _rewriteIndexes = configuration.RewriteIndexes;

            ReportEverySec = configuration.TimeReportSec;
        }

        public string CollectionName { get; protected set; }

        public virtual async Task<string> StatusTotalAsync()
        {
            var countDatabase = await CountEstimatedAsync();
            return $"Total count documents in {CollectionName}: {countDatabase.ToString("N0", new CultureInfo("en-us"))}.";
        }

        public async Task CreateIndexesAsync()
        {
            if (_rewriteIndexes)
            {
                await Collection.Indexes.DropAllAsync();
                Console.WriteLine($"Indexes of {CollectionName} removed.");
                Console.WriteLine($"Indexes of {CollectionName} creating. Please wait..");
            }

            await Collection.Indexes.CreateManyAsync(IndexModels);

            if (_rewriteIndexes)
            {
                Console.WriteLine($"Indexes of {CollectionName} created.");
            }
        }

        public virtual async Task DeleteIfSetAsync()
        {
            if (_clearDbBeforeSeed)
            {
                TimerStart();
                Console.WriteLine($"Documents of {CollectionName} deleting. Please wait..");
                await DeleteAsync();
                Console.WriteLine($"{CollectionName} is empty.");
                await TimerDisposeAsync();
            }
        }

        public virtual async Task SeedAsync()
        {
            if (ReportEverySec > 0)
            {
                TimerStart();
            }
        }

        protected uint ReportEverySec { get; }

        protected virtual async Task DeleteAsync()
        {
            await Collection.DeleteManyAsync(FilterDefinition<TCollection>.Empty);
        }

        protected virtual async Task StatusAsync()
        {
            var countDatabase = await CountEstimatedAsync();
            Console.WriteLine($"Current {CollectionName} count: {countDatabase.ToString("N0", new CultureInfo("en-us"))}");
        }
        
        protected void TimerStart()
        {
            async void TimerCallback(object? state) =>
                await StatusAsync();

            _timer = new Timer(TimerCallback, null, 0, ReportEverySec * 1000);
        }

        protected async Task TimerDisposeAsync()
        {
            if (_timer != null)
            {
                await _timer.DisposeAsync();
            }
        }

        private async Task<long> CountEstimatedAsync()
        {
            return await Collection.EstimatedDocumentCountAsync();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
