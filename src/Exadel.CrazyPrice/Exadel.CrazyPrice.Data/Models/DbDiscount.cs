using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models.Promocode;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace Exadel.CrazyPrice.Data.Models
{
    /// <summary>
    /// Represents the DbDiscount.
    /// </summary>
    public class DbDiscount
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? AmountOfDiscount { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public DateTime? StartDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public DateTime? EndDate { get; set; }

        [BsonIgnoreIfNull]
        public DbAddress Address { get; set; }

        [BsonIgnoreIfNull]
        public DbCompany Company { get; set; }

        [BsonIgnoreIfNull]
        public string WorkingDaysOfTheWeek { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Tags { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.Decimal128)]
        [JsonIgnore]
        public decimal? RatingTotal { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public int? ViewsTotal { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public int? SubscriptionsTotal { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public int? UsersSubscriptionTotal { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> FavoritesUsersId { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<DbUserPromocodes> UsersPromocodes { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> RatingUsersId { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DbUser UserCreateDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DateTime? LastChangeDate { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DbUser UserLastChangeDate { get; set; }

        [BsonDefaultValue(false)]
        [JsonIgnore]
        public bool Deleted { get; set; }

        [BsonIgnoreIfNull]
        public string PictureUrl { get; set; }

        [BsonIgnoreIfNull]
        public DbPromocodeOptions PromocodeOptions { get; set; }

        [BsonIgnoreIfNull]
        public string Language { get; set; }

        [BsonIgnoreIfNull]
        public List<DbTranslation> Translations { get; set; }

        /// <summary>
        /// Added the promocode for user.
        /// </summary>
        /// <param name="userUid"></param>
        /// <param name="dbPromocode"></param>
        public DbDiscount AddUserPromocode(Guid userUid, DbPromocode dbPromocode)
        {
            var uidUser = userUid.ToString();

            var dbUserPromocodes = new DbUserPromocodes
            {
                UserId = uidUser,
                Promocodes = new List<DbPromocode> { dbPromocode }
            };

            if (UsersPromocodes.IsEmpty())
            {
                UsersPromocodes = new List<DbUserPromocodes> { dbUserPromocodes };
                return this;
            }

            var newUserPromocodes = UsersPromocodes.FirstOrDefault(i => i.UserId == uidUser);
            if (newUserPromocodes == null)
            {
                UsersPromocodes.Add(dbUserPromocodes);
                return this;
            }

            newUserPromocodes.Promocodes.Add(dbPromocode);
            return this;
        }

        /// <summary>
        /// Fix spam.
        /// </summary>
        /// <param name="userUid"></param>
        /// <param name="dateTimeNow"></param>
        /// <returns></returns>
        public bool CanAddUserPromocode(Guid userUid, DateTime dateTimeNow)
        {
            var userPromocodes = UsersPromocodes?.FirstOrDefault(p => p.UserId == userUid.ToString());

            return (userPromocodes == null ||
                    userPromocodes.CanAdd(PromocodeOptions.TimeLimitAddingInSeconds, PromocodeOptions.CountActivePromocodePerUser, dateTimeNow));
        }

        /// <summary>
        /// Gets true when promocode deleted marked true otherwise false.
        /// </summary>
        /// <param name="userUid"></param>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        public bool RemoveUserPromocode(Guid userUid, Guid promocodeId)
        {
            var userPromocodes = UsersPromocodes?.FirstOrDefault(p => p.UserId == userUid.ToString());

            return userPromocodes != null && userPromocodes.RemovePromocode(promocodeId);
        }

        /// <summary>
        /// Gets the user promocodes when they are otherwise return <c>null</c>.
        /// </summary>
        /// <param name="userUid"></param>
        /// <returns></returns>
        public DbUserPromocodes GetDbUserPromocodes(Guid userUid) =>
            UsersPromocodes?.FirstOrDefault(i => i.UserId == userUid.ToString());

        /// <summary>
        /// Gets count promocodes.
        /// </summary>
        public int GetCountPromocodes() =>
            UsersPromocodes?.Sum(x => x.Promocodes?.Count ?? 0) ?? 0;


        /// <summary>
        /// Gets count users with promocodes.
        /// </summary>
        public int GetCountUsersWithPromocodes() =>
            UsersPromocodes?.Count ?? 0;


    }
}
