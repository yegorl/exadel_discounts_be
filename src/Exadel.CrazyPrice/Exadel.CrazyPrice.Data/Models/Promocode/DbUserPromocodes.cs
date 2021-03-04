using Exadel.CrazyPrice.Data.Extentions;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exadel.CrazyPrice.Data.Models.Promocode
{
    public class DbUserPromocodes
    {
        [BsonIgnoreIfNull]
        public string UserId { get; set; }

        [BsonIgnoreIfNull]
        public List<DbPromocode> Promocodes { get; set; }

        /// <summary>
        /// Returns true when time since last addition great than timeLimit seconds
        /// and count of promocodes less than countLimit otherwise false.
        /// </summary>
        /// <param name="dbPromocodeOptions"></param>
        /// <param name="dateTimeNow"></param>
        /// <returns></returns>
        public bool CanAdd(DbPromocodeOptions dbPromocodeOptions, DateTime dateTimeNow)
        {
            var timeLimit = dbPromocodeOptions.TimeLimitAddingInSeconds;
            var countLimit = dbPromocodeOptions.CountActivePromocodePerUser;
            var enabledPromocodes = dbPromocodeOptions.EnabledPromocodes ?? false;

            if (!enabledPromocodes)
            {
                return false;
            }

            if (countLimit == null)
            {
                return IsTimeLimitExpired(timeLimit, dateTimeNow);
            }

            return (IsTimeLimitExpired(timeLimit, dateTimeNow)
                    && (Promocodes.IsEmpty()
                    || Promocodes
                        .Where(i => i.Deleted == false && i.EndDate > dateTimeNow)
                        .ToList()
                        .Count < countLimit));
        }

        /// <summary>
        /// Gets true when promocode deleted marked true otherwise false.
        /// </summary>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        public bool RemovePromocode(Guid promocodeId)
        {
            if (Promocodes.IsEmpty())
            {
                return false;
            }

            var promocode = Promocodes.FirstOrDefault(i => i.Id == promocodeId.ToString());
            if (promocode == null)
            {
                return false;
            }

            promocode.Deleted = true;
            return true;
        }

        private bool IsTimeLimitExpired(int? timeLimit, DateTime dateTimeNow) =>
            Promocodes?.All(p => dateTimeNow - p.CreateDate > TimeSpan.FromSeconds(timeLimit ?? 1)) ?? true;
    }
}