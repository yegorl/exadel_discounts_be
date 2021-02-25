using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Models.Promocode;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    /// <summary>
    /// Represents the discount repository.
    /// </summary>
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IMongoCollection<DbDiscount> _discounts;
        private readonly IMongoCollection<DbTag> _tags;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Creates the discount repository.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userRepository"></param>
        public DiscountRepository(IDbConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository;

            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);

            _discounts = db.GetCollection<DbDiscount>("Discounts");
            _tags = db.GetCollection<DbTag>("Tags");
        }

        /// <summary>
        /// Gets discounts by search criteria.
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public async Task<List<Discount>> GetDiscountsAsync(SearchCriteria searchCriteria)
        {
            var discounts = new List<Discount>();

            using var cursor = await _discounts.FindAsync(searchCriteria.GetQuery(),
                new FindOptions<DbDiscount>
                {
                    Limit = searchCriteria.SearchPaginationCountElementPerPage,
                    Skip = searchCriteria.GetSkip(),
                    Sort = new JsonSortDefinition<DbDiscount>(searchCriteria.GetSort())
                });

            while (await cursor.MoveNextAsync())
            {
                discounts.AddRange(cursor.Current.Select(item => item.ToDiscount()));
            }

            return discounts;
        }

        /// <summary>
        /// Gets discounts by uid.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<Discount> GetDiscountByUidAsync(Guid uid)
        {
            var stringGuid = uid.ToString();

            return (await _discounts.FindOneAndUpdateAsync(
                    Builders<DbDiscount>.Filter.Where(d => d.Id == stringGuid),
                    Builders<DbDiscount>.Update
                        .Inc(f => f.ViewsTotal, 1),
                    new FindOneAndUpdateOptions<DbDiscount>()
                    {
                        ReturnDocument = ReturnDocument.After
                    }))
                .ToDiscount();
        }

        /// <summary>
        /// Create or update the discount.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public async Task<Discount> UpsertDiscountAsync(Discount discount)
        {
            var discountFromDb = await GetDbDiscountByUidAsync(discount.Id);

            DbDiscount dbDiscountResult;

            if (discountFromDb.IsEmpty())
            {
                await _discounts.InsertOneAsync(discount.ToDbDiscount());
                dbDiscountResult = await GetDbDiscountByUidAsync(discount.Id);
            }
            else
            {
                if (discountFromDb.Deleted)
                {
                    return new Discount();
                }

                dbDiscountResult = await _discounts.FindOneAndUpdateAsync(
                Builders<DbDiscount>.Filter.Where(d => d.Id == discount.Id.ToString()),
                Builders<DbDiscount>.Update
                    .Set(f => f.Name, discount.Name)
                    .Set(f => f.Description, discount.Description)
                    .Set(f => f.AmountOfDiscount, discount.AmountOfDiscount)
                    .Set(f => f.StartDate, discount.StartDate)
                    .Set(f => f.EndDate, discount.EndDate)
                    .Set(f => f.Address, discount.Address.ToDbAddress())
                    .Set(f => f.Company, discount.Company.ToDbCompany())
                    .Set(f => f.WorkingDaysOfTheWeek, discount.WorkingDaysOfTheWeek)
                    .Set(f => f.Tags, discount.Tags)
                    .Set(f => f.LastChangeDate, discount.LastChangeDate)
                    .Set(f => f.UserLastChangeDate, discount.UserLastChangeDate.ToDbUser())
                    .Set(f => f.PictureUrl, discount.PictureUrl)
                    .Set(f => f.Language, discount.Language.ToStringLookup())
                    .Set(f => f.Translations, discount.Translations.ToDbTranslations())
                , new FindOneAndUpdateOptions<DbDiscount>()
                {
                    ReturnDocument = ReturnDocument.After
                });
            }

            await UpdateTags(dbDiscountResult);

            return dbDiscountResult.ToDiscount();
        }

        /// <summary>
        /// Sets the discount as deleted.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        public async Task RemoveDiscountByUidAsync(Guid uid, Guid userUid)
        {
            var dbUser = await GetUserByUidAsync(userUid);

            await _discounts.UpdateOneAsync(
                 d => d.Id == uid.ToString(),
                 Builders<DbDiscount>.Update
                     .Set(f => f.Deleted, true)
                     .Set(f => f.LastChangeDate, DateTime.Now)
                     .Set(f => f.UserLastChangeDate, dbUser)
                 , new UpdateOptions { IsUpsert = true });
        }

        /// <summary>
        /// Sets discounts as deleted.
        /// </summary>
        /// <param name="uids"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        public async Task RemoveDiscountAsync(List<Guid> uids, Guid userUid)
        {
            var dbUser = await GetUserByUidAsync(userUid);

            await _discounts.UpdateManyAsync(
                Builders<DbDiscount>.Filter.In(d => d.Id, uids.Select(i => i.ToString()).ToList()),
                Builders<DbDiscount>.Update
                    .Set(f => f.Deleted, true)
                    .Set(f => f.LastChangeDate, DateTime.Now)
                    .Set(f => f.UserLastChangeDate, dbUser)
                , new UpdateOptions { IsUpsert = true });
        }

        /// <summary>
        /// Updates tags from discount.
        /// </summary>
        /// <param name="dbDiscount"></param>
        /// <returns></returns>
        public async Task UpdateTags(DbDiscount dbDiscount)
        {
            if (dbDiscount == null)
            {
                return;
            }

            if (!dbDiscount.Tags.IsNullOrEmpty())
            {
                var tagTranslations = dbDiscount.Tags.Select(dbDiscountTag =>
                                new TagTranslations { TagName = dbDiscountTag, Language = dbDiscount.Language }).ToList();

                await UpsertTags(tagTranslations,
                                tag => Builders<DbTag>.Update
                                    .Set(f => f.Name, tag.TagName)
                                    .Set(f => f.Language, tag.Language));
            }

            if (!dbDiscount.Translations.IsEmpty())
            {
                var tagOtherTranslations = new List<TagTranslations>();
                foreach (var dbDiscountTranslation in dbDiscount.Translations)
                {
                    var tags = dbDiscountTranslation.Tags;
                    if (tags.IsNullOrEmpty())
                    {
                        continue;
                    }

                    foreach (var tag in tags)
                    {
                        tagOtherTranslations.Add(new TagTranslations { TagName = tag, Language = dbDiscountTranslation.Language });
                    }
                }

                if (tagOtherTranslations.Count == 0)
                {
                    return;
                }

                await UpsertTags(tagOtherTranslations,
                    tag => Builders<DbTag>.Update
                        .Set(f => f.Name, tag.TagName)
                        .Set(f => f.Translations, new List<DbTag>
                        {
                        new()
                        {
                            Name = tag.TagName,
                            Language = tag.Language
                        }
                        }));
            }
        }

        /// <summary>
        /// Gets DbUser by uid.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<DbUser> GetUserByUidAsync(Guid uid)
        {
            var user = await _userRepository.GetUserByUidAsync(uid);
            user.Roles = RoleOption.Unknown;
            user.HashPassword = null;
            user.Salt = null;

            return user.ToDbUser();
        }

        public async Task<bool> VoteDiscountAsync(int value, Guid discountUid, Guid userUid)
        {
            var uidUser = userUid.ToString();

            var dbDiscount = await GetDbDiscountByUidAsync(discountUid);
            if (dbDiscount.Deleted)
            {
                return false;
            }

            var voteUsers = dbDiscount.RatingUsersId;

            if (voteUsers == null)
            {
                voteUsers = new List<string>();
            }
            else if (voteUsers.Contains(uidUser))
            {
                return false;
            }

            voteUsers.Add(uidUser);
            var countVoteUsers = voteUsers.Count;

            var newRating = (dbDiscount.RatingTotal * countVoteUsers + value) / (countVoteUsers + 1);

            await _discounts.UpdateOneAsync(
                d => d.Id == discountUid.ToString(),
                Builders<DbDiscount>.Update
                    .Set(f => f.RatingTotal, newRating)
                    .Set(f => f.RatingUsersId, voteUsers)
                , new UpdateOptions { IsUpsert = true });

            return true;
        }

        #region Favorites
        public async Task AddToFavoritesAsync(Guid discountUid, Guid userUid)
        {
            var dbDiscount = await GetDbDiscountByUidAsync(discountUid);

            if (dbDiscount.Deleted)
            {
                return;
            }

            var favoritesUsersId = dbDiscount.FavoritesUsersId;

            if (AddUserInList(userUid, ref favoritesUsersId))
            {
                return;
            }

            await UpsertFavorites(discountUid, favoritesUsersId);
        }

        public async Task RemoveFromFavoritesAsync(Guid discountUid, Guid userUid)
        {
            var dbDiscount = await GetDbDiscountByUidAsync(discountUid);
            var favoritesUsersId = dbDiscount.FavoritesUsersId;

            if (RemoveUserFromList(userUid, ref favoritesUsersId))
            {
                return;
            }

            await UpsertFavorites(discountUid, favoritesUsersId);
        }
        #endregion

        #region Subscriptions

        public async Task<UserPromocodes> AddToSubscriptionsAsync(Guid discountUid, Guid userUid)
        {
            var dbDiscount = await GetDbDiscountByUidAsync(discountUid);

            if (dbDiscount.Deleted)
            {
                return new UserPromocodes();
            }

            if (!dbDiscount.CanAddUserPromocode(userUid, DateTime.UtcNow))
            {
                return dbDiscount.GetDbUserPromocodes(userUid).ToUserPromocodes();
            }

            var newPromocode = DbPromocode.New(Guid.NewGuid(), DateTime.Now, dbDiscount.PromocodeOptions.DaysDurationPromocode, StringExtentions.NewPromocodeValue(dbDiscount.PromocodeOptions?.CountSymbolsPromocode ?? 4));

            dbDiscount.AddUserPromocode(userUid, newPromocode);

            await UpsertSubscriptions(dbDiscount);

            return dbDiscount.GetDbUserPromocodes(userUid).ToUserPromocodes();
        }

        public async Task<UserPromocodes> GetSubscriptionsAsync(Guid discountUid, Guid userUid)
        {
            var dbDiscount = await GetDbDiscountByUidAsync(discountUid);
            if (dbDiscount.Deleted)
            {
                return new UserPromocodes();
            }

            return dbDiscount.GetDbUserPromocodes(userUid).ToUserPromocodes();
        }

        public async Task<UserPromocodes> RemoveFromSubscriptionsAsync(Guid discountUid, Guid userUid, Guid promocodeId)
        {
            var dbDiscount = await GetDbDiscountByUidAsync(discountUid);

            if (dbDiscount.RemoveUserPromocode(userUid, promocodeId))
            {
                await UpsertSubscriptions(dbDiscount);
            }

            return dbDiscount.GetDbUserPromocodes(userUid).ToUserPromocodes();
        }
        #endregion

        #region private methods
        private async Task UpsertSubscriptions(DbDiscount dbDiscount) =>
            await _discounts.UpdateOneAsync(
                d => d.Id == dbDiscount.Id,
                Builders<DbDiscount>.Update
                    .Set(f => f.UsersPromocodes, dbDiscount.UsersPromocodes)
                    .Set(f => f.SubscriptionsTotal, dbDiscount.GetCountPromocodes())
                    .Set(f => f.UsersSubscriptionTotal, dbDiscount.GetCountUsersWithPromocodes())
                , new UpdateOptions { IsUpsert = true });

        private async Task UpsertFavorites(Guid discountUid, List<string> usersIdList) =>
            await _discounts.UpdateOneAsync(
                d => d.Id == discountUid.ToString(),
                Builders<DbDiscount>.Update
                    .Set(f => f.FavoritesUsersId, usersIdList)
                , new UpdateOptions { IsUpsert = true });

        private static bool AddUserInList(Guid userUid, ref List<string> list)
        {
            var uidUser = userUid.ToString();

            if (list == null)
            {
                list = new List<string>();
            }
            else if (list.Contains(uidUser))
            {
                return true;
            }

            list.Add(uidUser);
            return false;
        }

        private static bool RemoveUserFromList(Guid userUid, ref List<string> subscriptionsUsersId)
        {
            var uidUser = userUid.ToString();

            if (subscriptionsUsersId == null || !subscriptionsUsersId.Contains(uidUser))
            {
                return true;
            }

            subscriptionsUsersId.Remove(uidUser);
            return false;
        }

        private async Task<DbDiscount> GetDbDiscountByUidAsync(Guid id) =>
            (await _discounts.FindSync(Builders<DbDiscount>.Filter.Eq(d => d.Id, id.ToString()),
                new FindOptions<DbDiscount> { Limit = 1 }).ToListAsync()).GetOne();

        private async Task UpsertTags(List<TagTranslations> tagTranslations, Func<TagTranslations, UpdateDefinition<DbTag>> updateDefinition)
        {
            foreach (var tag in tagTranslations)
            {
                var update = updateDefinition(tag);
                await _tags.UpdateOneAsync(dbTag => dbTag.Name == tag.TagName, update, new UpdateOptions { IsUpsert = true });
            }
        }

        private class TagTranslations
        {
            public string TagName { get; set; }

            public string Language { get; set; }
        }
        #endregion
    }
}
