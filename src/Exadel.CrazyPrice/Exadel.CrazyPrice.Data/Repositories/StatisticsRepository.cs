using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Common.Models.Statistics;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IMongoCollection<DbDiscount> _discounts;
        private readonly IMongoCollection<DbUser> _users;

        public StatisticsRepository(IDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);
            _discounts = db.GetCollection<DbDiscount>("Discounts");
            _users = db.GetCollection<DbUser>("Users");
        }

        public async Task<DiscountsStatistics> GetDiscountsStatistics(DiscountsStatisticsCriteria criteria)
        {
            var aggregate = await _discounts.Aggregate().Match(criteria.GetMatch()).Group(criteria.GetGroup()).ToListAsync();

            var statistics = new DiscountsStatistics();
            if (aggregate.Count == 0)
            {
                return null;
            }

            var doc = aggregate[0];

            var discountTotal = doc.GetValue("discountTotal").AsInt32;
            var viewsTotal = doc.GetValue("viewsTotal").AsInt32;
            var subscriptionsTotal = doc.GetValue("subscriptionsTotal").AsInt32;
            var RatedTotal = doc.GetValue("ratedTotal").AsInt32;
            var inSubscriptionsList = doc.GetValue("inSubscriptionsList").AsInt32;

            statistics.DiscountsTotal = discountTotal;
            statistics.ViewsTotal = viewsTotal;
            statistics.SubscriptionsTotal = subscriptionsTotal;
            statistics.RatedTotal = RatedTotal;
            statistics.InSubscriptionsListTotal = inSubscriptionsList;
            return statistics;
        }

        public async Task<UsersStatistics> GetUsersStatistics()
        {
            var groupStr = "{\"_id\": \"$roles\", \"count\": {$sum: 1}}";

            var aggregate = await _users.Aggregate().Group(groupStr).ToListAsync();

            var statistics = new UsersStatistics();
            foreach (var doc in aggregate)
            {
                var role = (RoleOption)doc.GetValue("_id").AsInt32;
                var count = doc.GetValue("count").AsInt32;

                switch (role)
                {
                    case RoleOption.Employee:
                        statistics.EmployeesTotal = count;
                        break;
                    case RoleOption.Moderator:
                        statistics.ModeratorsTotal = count;
                        break;
                    case RoleOption.Administrator:
                        statistics.AdministratorsTotal = count;
                        break;
                    default:
                        statistics.UnknownsTotal += count;
                        break;
                }
            }
            return statistics;
        }
    }
}
