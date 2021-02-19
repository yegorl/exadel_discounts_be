using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Common.Models.Statistic;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly IMongoCollection<DbDiscount> _discounts;
        private readonly IMongoCollection<DbUser> _users;

        public StatisticRepository(IDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);
            _discounts = db.GetCollection<DbDiscount>("Discounts");
            _users = db.GetCollection<DbUser>("Users");
        }

        public async Task<DiscountsStatistic> GetDiscountsStatistic(DiscountsStatisticCriteria criteria)
        {
            var aggregate = await _discounts.Aggregate().Match(criteria.GetMatch()).Group(criteria.GetGroup()).ToListAsync();

            var statistic = new DiscountsStatistic();
            if (aggregate.Count == 0) return null;
            else
            {
                var doc = aggregate[0];

                var discountTotal = doc.GetValue("discountTotal").AsInt32;
                var viewsTotal = doc.GetValue("viewsTotal").AsInt32;
                var subscriptionsTotal = doc.GetValue("subscriptionsTotal").AsInt32;
                var inFavoritesList = doc.GetValue("inFavoritesList").AsInt32;
                var inSubscriptionsList = doc.GetValue("inSubscriptionsList").AsInt32;

                statistic.DiscountsTotal = discountTotal;
                statistic.ViewsTotal = viewsTotal;
                statistic.SubscriptionsTotal = subscriptionsTotal;
                statistic.InFavoritesListTotal = inFavoritesList;
                statistic.InSubscriptionsListTotal = inSubscriptionsList;
                return statistic;
            }
        }

        public async Task<UsersStatistic> GetUsersStatistic()
        {
            var groupStr = "{\"_id\": \"$roles\", \"count\": {$sum: 1}}";

            var aggregate = await _users.Aggregate().Group(groupStr).ToListAsync();

            var statistic = new UsersStatistic();
            foreach (var doc in aggregate)
            {
                var role = (RoleOption) doc.GetValue("_id").AsInt32;
                var count = doc.GetValue("count").AsInt32;

                switch (role)
                {
                    case RoleOption.Employee:
                        statistic.EmployeesTotal = count;
                        break;
                    case RoleOption.Moderator:
                        statistic.ModeratorsTotal = count;
                        break;
                    case RoleOption.Administrator:
                        statistic.AdministratorsTotal = count;
                        break;
                    default: statistic.UnknownsTotal += count;
                        break;
                }
            }
            return statistic;
        }
    }
}
