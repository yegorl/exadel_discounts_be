using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IMongoCollection<DbDiscount> _discounts;

        public DiscountRepository(IOptions<MongoDbConfiguration> configuration)
        {
            var client = new MongoClient(configuration.Value.ConnectionString);
            var db = client.GetDatabase(configuration.Value.Database);

            _discounts = db.GetCollection<DbDiscount>("Discounts");
        }

        public async Task<List<DiscountResponse>> GetDiscountsAsync(SearchCriteria searchCriteria)
        {

            throw new NotImplementedException();
        }

        public async Task<DiscountResponse> GetDiscountByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
        }

        public async Task<UpsertDiscountRequest> UpsertDiscountAsync(UpsertDiscountRequest item)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDiscountByUidAsync(Guid uid) =>
            await _discounts.DeleteOneAsync(Builders<DbDiscount>.Filter.Eq("_id", uid.ToString()));

        public async Task RemoveDiscountAsync(List<Guid> uids) =>
            await _discounts.DeleteManyAsync(Builders<DbDiscount>.Filter.In("_id", uids.Select(i => i.ToString()).ToList()));
    }
}
