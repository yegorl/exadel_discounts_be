using Exadel.CrazyPrice.Data.Seeder.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models.MongoSeed
{
    /// <summary>
    /// Represents methods for seed.
    /// </summary>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class MongoAbstractSeed<TCollection> : AbstractSeed where TCollection : class
    {
        protected IMongoCollection<TCollection> Collection;
        protected List<CreateIndexModel<TCollection>> IndexModels;

        protected MongoAbstractSeed(SeederConfiguration configuration) : base(configuration)
        {
        }

        public override async Task CreateIndexesAsync()
        {
            if (RewriteIndexes)
            {
                await Collection.Indexes.DropAllAsync();
                Console.WriteLine($"Indexes of {CollectionName} removed.");
                Console.WriteLine($"Indexes of {CollectionName} creating. Please wait..");
            }

            await Collection.Indexes.CreateManyAsync(IndexModels);

            if (RewriteIndexes)
            {
                Console.WriteLine($"Indexes of {CollectionName} created.");
            }
        }

        protected override async Task DeleteAsync()
        {
            await Collection.DeleteManyAsync(FilterDefinition<TCollection>.Empty);
        }

        protected override async Task<long> CountEstimatedAsync()
        {
            return await Collection.EstimatedDocumentCountAsync();
        }
    }
}
