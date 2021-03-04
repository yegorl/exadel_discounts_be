using Exadel.CrazyPrice.Data.Seeder.Configuration;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models
{
    public abstract class AbstractSeed : IDisposable, ISeed
    {
        private Timer _timer;
        private readonly bool _clearDbBeforeSeed;

        protected readonly bool RewriteIndexes;
        protected uint DefaultCountSeed;

        protected AbstractSeed(SeederConfiguration configuration)
        {
            _clearDbBeforeSeed = configuration.ClearDataBeforeSeed;
            RewriteIndexes = configuration.RewriteIndexes;

            ReportEverySec = configuration.TimeReportSec;
        }

        public string CollectionName { get; protected set; }

        public virtual async Task<string> StatusTotalAsync()
        {
            var countDatabase = await CountEstimatedAsync();
            return $"Total count documents in {CollectionName}: {countDatabase.ToString("N0", new CultureInfo("en-us"))}.";
        }

        public virtual async Task DeleteIfSetAsync()
        {
            if (_clearDbBeforeSeed)
            {
                Console.WriteLine($"Documents of {CollectionName} deleting. Please wait..");
                await DeleteAsync();
                Console.WriteLine($"{CollectionName} is empty.");
                await TimerDisposeAsync();
            }
        }

        public virtual async Task SeedAsync()
        {
            await CreateIndexesAsync();
            if (ReportEverySec > 0)
            {
                TimerStart();
            }
        }

        public abstract Task CreateIndexesAsync();

        protected abstract Task DeleteAsync();

        protected abstract Task<long> CountEstimatedAsync();

        protected uint ReportEverySec { get; }

        protected async Task TimerDisposeAsync()
        {
            if (_timer != null)
            {
                await _timer.DisposeAsync();
            }
        }

        protected void TimerStart()
        {
            async void TimerCallback(object state) =>
                await StatusAsync();

            _timer = new Timer(TimerCallback, null, 0, ReportEverySec * 1000);
        }

        private async Task StatusAsync()
        {
            var countDatabase = await CountEstimatedAsync();
            Console.WriteLine($"Current {CollectionName} count: {countDatabase.ToString("N0", new CultureInfo("en-us"))}");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
