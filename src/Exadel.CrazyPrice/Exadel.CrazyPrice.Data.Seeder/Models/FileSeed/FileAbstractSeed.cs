using Exadel.CrazyPrice.Data.Seeder.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models.FileSeed
{
    /// <summary>
    /// Represents methods for seed.
    /// </summary>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class FileAbstractSeed<TCollection> : AbstractSeed where TCollection : class
    {
        private readonly SeederConfiguration _configuration;
        protected List<TCollection> Collection;

        protected FileAbstractSeed(SeederConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public override async Task CreateIndexesAsync()
        {
        }

        protected string FullName => Path.Combine(_configuration.Path, CollectionName);

        protected override async Task DeleteAsync()
        {
            File.Delete(FullName);
        }

        protected override async Task<long> CountEstimatedAsync()
        {
            return Collection.Count;
        }
    }
}
