using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Models.Promocode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exadel.CrazyPrice.Data.Extentions
{
    /// <summary>
    /// Represents extentions for DbDiscount.
    /// </summary>
    public static class DbDiscountExtentions
    {
        /// <summary>
        /// Gets one DbDiscount from discount list.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DbDiscount GetOne(this List<DbDiscount> values) =>
            values == null || values.Count == 0 ? new DbDiscount() : values[0];

        /// <summary>
        /// Gets Discount entity from DbDiscount entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Discount ToDiscount(this DbDiscount value)
        {
            if (value.IsEmpty())
            {
                return new Discount();
            }

            try
            {
                return new Discount
                {
                    Id = new Guid(value.Id),
                    Name = value.Name,
                    Description = value.Description,
                    AmountOfDiscount = value.AmountOfDiscount,
                    StartDate = value.StartDate,
                    EndDate = value.EndDate,
                    Address = value.Address.ToAddress(),
                    Company = value.Company.ToCompany(),
                    WorkingDaysOfTheWeek = value.WorkingDaysOfTheWeek,
                    Tags = value.Tags,
                    PictureUrl = value.PictureUrl,
                    Language = value.Language.ToLanguageOption(),
                    Translations = value.Translations.ToTranslations(),

                    RatingTotal = value.RatingTotal,
                    ViewsTotal = value.ViewsTotal,
                    SubscriptionsTotal = value.SubscriptionsTotal,
                    UsersSubscriptionTotal = value.UsersSubscriptionTotal,

                    FavoritesUsersId = value.FavoritesUsersId,
                    UsersPromocodes = value.UsersPromocodes.ToUsersPromocodes(),
                    PromocodeOptions = value.PromocodeOptions.ToPromocodeOptions(),

                    CreateDate = value.CreateDate,
                    LastChangeDate = value.LastChangeDate,

                    UserCreateDate = value.UserCreateDate.ToUser(),
                    UserLastChangeDate = value.UserLastChangeDate.ToUser(),

                    Deleted = value.Deleted
                };
            }
            catch
            {
                return new Discount();
            }
        }

        /// <summary>
        /// Gets DbDiscount entity from Discount entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbDiscount ToDbDiscount(this Discount value) =>
            new()
            {
                Id = value.Id.ToString(),
                Name = value.Name,
                Description = value.Description,
                AmountOfDiscount = value.AmountOfDiscount,
                StartDate = value.StartDate,
                EndDate = value.EndDate,
                Address = value.Address.ToDbAddress(),
                Company = value.Company.ToDbCompany(),
                WorkingDaysOfTheWeek = value.WorkingDaysOfTheWeek,
                Tags = value.Tags,
                PictureUrl = value.PictureUrl,
                Language = value.Language.ToStringLookup(),
                Translations = value.Translations.ToDbTranslations(),

                RatingTotal = value.RatingTotal,
                ViewsTotal = value.ViewsTotal,
                SubscriptionsTotal = value.SubscriptionsTotal,
                UsersSubscriptionTotal = value.UsersSubscriptionTotal,

                FavoritesUsersId = value.FavoritesUsersId,
                UsersPromocodes = value.UsersPromocodes.ToDbUsersPromocodes(),
                PromocodeOptions = value.PromocodeOptions.ToDbPromocodeOptions(),

                CreateDate = value.CreateDate,
                LastChangeDate = value.LastChangeDate,

                UserCreateDate = value.UserCreateDate.ToDbUser(),
                UserLastChangeDate = value.UserLastChangeDate.ToDbUser(),

                Deleted = value.Deleted
            };

        /// <summary>
        /// Gets Address entity from DbAddress entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Address ToAddress(this DbAddress value) =>
            value.IsEmpty() ? null : new Address
            {
                Country = value.Country,
                City = value.City,
                Street = value.Street,
                Location = value.Location.ToLocation()
            };

        /// <summary>
        /// Gets TranslationAddress entity from DbAddress entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TranslationAddress ToTranslationAddress(this DbTranslationAddress value) =>
            value == null ? null : new TranslationAddress
            {
                Country = value.Country,
                City = value.City,
                Street = value.Street
            };

        /// <summary>
        /// Gets DbAddress entity from Address entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbAddress ToDbAddress(this Address value) =>
            value.IsEmpty() ? null : new DbAddress
            {
                Country = value.Country,
                City = value.City,
                Street = value.Street,
                Location = value.Location.ToDbLocation()
            };

        /// <summary>
        /// Gets DbTranslationAddress entity from Address entity.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbTranslationAddress ToDbTranslationAddress(this TranslationAddress value) =>
            value == null ? null : new DbTranslationAddress
            {
                Country = value.Country,
                City = value.City,
                Street = value.Street
            };

        /// <summary>
        /// Gets Location entity from DbLocation entity.
        /// </summary>
        /// <param name="dbLocation"></param>
        /// <returns></returns>
        public static Location ToLocation(this DbLocation dbLocation) =>
            dbLocation.IsEmpty() ? null : new Location
            {
                Longitude = dbLocation.Longitude,
                Latitude = dbLocation.Latitude
            };

        /// <summary>
        /// Gets DbLocation entity from Location entity.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static DbLocation ToDbLocation(this Location location) =>
            location.IsEmpty() ? null : new DbLocation
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };

        /// <summary>
        /// Gets Company entity from DbCompany entity.
        /// </summary>
        /// <param name="dbCompany"></param>
        /// <returns></returns>
        public static Company ToCompany(this DbCompany dbCompany) =>
            dbCompany.IsEmpty() ? null : new Company
            {
                Name = dbCompany.Name,
                Description = dbCompany.Description,
                PhoneNumber = dbCompany.PhoneNumber,
                Mail = dbCompany.Mail
            };

        /// <summary>
        /// Gets Translation Company entity from DbCompany entity.
        /// </summary>
        /// <param name="dbCompany"></param>
        /// <returns></returns>
        public static TranslationCompany ToTranslationCompany(this DbTranslationCompany dbCompany) =>
            dbCompany == null ? null : new TranslationCompany
            {
                Name = dbCompany.Name,
                Description = dbCompany.Description
            };

        /// <summary>
        /// Gets DbCompany entity from Company entity.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static DbCompany ToDbCompany(this Company company) =>
            company.IsEmpty() ? null : new DbCompany
            {
                Name = company.Name,
                Description = company.Description,
                PhoneNumber = company.PhoneNumber,
                Mail = company.Mail
            };

        /// <summary>
        /// Gets DbTranslationCompany entity from Company entity.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static DbTranslationCompany ToDbTranslationCompany(this TranslationCompany company) =>
            company == null ? null : new DbTranslationCompany
            {
                Name = company.Name,
                Description = company.Description
            };

        /// <summary>
        /// Gets List Translation from List DbTranslation.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<Translation> ToTranslations(this List<DbTranslation> values) =>
            values.IsEmpty() ? null : values.Select(dbTranslation => new Translation
            {
                Language = dbTranslation.Language.ToLanguageOption(),
                Name = dbTranslation.Name,
                Tags = dbTranslation.Tags,
                Description = dbTranslation.Description,
                Address = dbTranslation.Address.ToTranslationAddress(),
                Company = dbTranslation.Company.ToTranslationCompany()
            })
                .ToList();

        /// <summary>
        /// Gets List DbTranslation from List Translation.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<DbTranslation> ToDbTranslations(this List<Translation> values) =>
            values.IsEmpty() ? null : values.Select(dbTranslation => new DbTranslation
            {
                Language = dbTranslation.Language.ToStringLookup(),
                Name = dbTranslation.Name,
                Tags = dbTranslation.Tags,
                Description = dbTranslation.Description,
                Address = dbTranslation.Address.ToDbTranslationAddress(),
                Company = dbTranslation.Company.ToDbTranslationCompany()
            })
                .ToList();

        /// <summary>
        /// Gets Promocode from DbPromocode.
        /// </summary>
        /// <param name="dbPromocode"></param>
        /// <returns></returns>
        public static Promocode ToPromocode(this DbPromocode dbPromocode) =>
            dbPromocode.IsEmpty() ? null : new Promocode
            {
                Id = new Guid(dbPromocode.Id),
                CreateDate = dbPromocode.CreateDate,
                EndDate = dbPromocode.EndDate,
                PromocodeValue = dbPromocode.PromocodeValue,
                Deleted = dbPromocode.Deleted
            };

        /// <summary>
        /// Gets DbPromocode from Promocode.
        /// </summary>
        /// <param name="promocode"></param>
        /// <returns></returns>
        public static DbPromocode ToDbPromocode(this Promocode promocode) =>
            promocode.IsEmpty() ? null : new DbPromocode
            {
                Id = promocode.Id.ToString(),
                CreateDate = promocode.CreateDate,
                EndDate = promocode.EndDate,
                PromocodeValue = promocode.PromocodeValue,
                Deleted = promocode.Deleted
            };

        /// <summary>
        /// Gets UserPromocodes from DbUserPromocodes.
        /// </summary>
        /// <param name="dbUserPromocodes"></param>
        /// <returns></returns>
        public static UserPromocodes ToUserPromocodes(this DbUserPromocodes dbUserPromocodes)
        {
            if (dbUserPromocodes.IsEmpty())
            {
                return null;
            }

            var userPromocodes = new UserPromocodes
            {
                UserId = new Guid(dbUserPromocodes.UserId),
                Promocodes = new List<Promocode>()
            };

            foreach (var promocode in dbUserPromocodes.Promocodes.Where(promocode => !promocode.Deleted))
            {
                userPromocodes.Promocodes.Add(promocode.ToPromocode());
            }

            return userPromocodes;
        }

        /// <summary>
        /// Gets DbUserPromocodes from UserPromocodes.
        /// </summary>
        /// <param name="userPromocodes"></param>
        /// <returns></returns>
        public static DbUserPromocodes ToDbUserPromocodes(this UserPromocodes userPromocodes)
        {
            if (userPromocodes.IsEmpty())
            {
                return null;
            }

            var promocodes = new DbUserPromocodes
            {
                UserId = userPromocodes.UserId.ToString(),
                Promocodes = new List<DbPromocode>()
            };

            foreach (var promocode in userPromocodes.Promocodes)
            {
                promocodes.Promocodes.Add(promocode.ToDbPromocode());
            }

            return promocodes;
        }

        /// <summary>
        /// Gets List UserPromocodes from List DbUserPromocodes.
        /// </summary>
        /// <param name="dbUserPromocodes"></param>
        /// <returns></returns>
        public static List<UserPromocodes> ToUsersPromocodes(this List<DbUserPromocodes> dbUserPromocodes)
        {
            if (dbUserPromocodes.IsEmpty())
            {
                return null;
            }

            var userPromocodes = new List<UserPromocodes>();

            userPromocodes.AddRange(dbUserPromocodes.Select(promocode => promocode.ToUserPromocodes()));

            return userPromocodes;
        }

        /// <summary>
        /// Gets List DbUserPromocodes from List UserPromocodes.
        /// </summary>
        /// <param name="userPromocodes"></param>
        /// <returns></returns>
        public static List<DbUserPromocodes> ToDbUsersPromocodes(this List<UserPromocodes> userPromocodes)
        {
            if (userPromocodes.IsEmpty())
            {
                return null;
            }

            var promocodes = new List<DbUserPromocodes>();
            promocodes.AddRange(userPromocodes.Select(promocode => promocode.ToDbUserPromocodes()));

            return promocodes;
        }

        /// <summary>
        /// Gets PromocodeOptions from DbPromocodeOptions.
        /// </summary>
        /// <param name="dbPromocodeOptions"></param>
        /// <returns></returns>
        public static PromocodeOptions ToPromocodeOptions(this DbPromocodeOptions dbPromocodeOptions) =>
            dbPromocodeOptions.IsEmpty() ? null : new PromocodeOptions
            {
                CountActivePromocodePerUser = dbPromocodeOptions.CountActivePromocodePerUser,
                CountSymbolsPromocode = dbPromocodeOptions.CountSymbolsPromocode,
                DaysDurationPromocode = dbPromocodeOptions.DaysDurationPromocode,
                TimeLimitAddingInSeconds = dbPromocodeOptions.TimeLimitAddingInSeconds
            };

        /// <summary>
        /// Gets DbPromocodeOptions from PromocodeOptions.
        /// </summary>
        /// <param name="promocodeOptions"></param>
        /// <returns></returns>
        public static DbPromocodeOptions ToDbPromocodeOptions(this PromocodeOptions promocodeOptions) =>
            promocodeOptions.IsEmpty() ? null : new DbPromocodeOptions
            {
                CountActivePromocodePerUser = promocodeOptions.CountActivePromocodePerUser,
                CountSymbolsPromocode = promocodeOptions.CountSymbolsPromocode,
                DaysDurationPromocode = promocodeOptions.DaysDurationPromocode,
                TimeLimitAddingInSeconds = promocodeOptions.TimeLimitAddingInSeconds
            };

        /// <summary>
        /// Gets true when the DbPromocodeOptions entity or all property is Null otherwise false.
        /// </summary>
        /// <param name="dbPromocodeOptions"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbPromocodeOptions dbPromocodeOptions) =>
            dbPromocodeOptions == null ||
            (dbPromocodeOptions.CountSymbolsPromocode == null
             && dbPromocodeOptions.TimeLimitAddingInSeconds == null
             && dbPromocodeOptions.CountActivePromocodePerUser == null
             && dbPromocodeOptions.DaysDurationPromocode == null);

        /// <summary>
        /// Gets true when the DbDiscount entity or id property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbDiscount value) =>
            value == null || value.Id.IsNullOrEmpty();

        /// <summary>
        /// Gets true when the DbAddress entity or all property is Null otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbAddress value) =>
            value == null ||
            (value.City.IsNullOrEmpty()
             && value.Country.IsNullOrEmpty()
             && value.Street.IsNullOrEmpty()
             && value.Location == null
            );

        /// <summary>
        /// Gets true when the DbLocation entity or all property is Null or 0 otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbLocation value) =>
            value == null ||
            (value.Longitude == 0
             && value.Latitude == 0
            );

        /// <summary>
        /// Gets true when the DbCompany entity or all property is Null otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbCompany value) =>
            value == null ||
            (value.Description.IsNullOrEmpty()
             && value.Mail.IsNullOrEmpty()
             && value.Name.IsNullOrEmpty()
             && value.PhoneNumber.IsNullOrEmpty()
            );

        /// <summary>
        /// Gets true when the List DbTranslation entity is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this List<DbTranslation> value) =>
            value == null || value.Count == 0;

        /// <summary>
        /// Gets true when the DbPromocode entity or id property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbPromocode value) =>
            value == null || value.Id.IsNullOrEmpty();

        /// <summary>
        /// Gets true when the List DbUserPromocodes entity is Null or Empty otherwise false.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsEmpty(this List<DbUserPromocodes> values) =>
            values == null || values.Count == 0;

        /// <summary>
        /// Gets true when the DbUserPromocodes entity or user id property is Null or Empty or Promocodes is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbUserPromocodes value) =>
            value == null || value.UserId.IsNullOrEmpty() || value.Promocodes == null || value.Promocodes.Count == 0;
    }
}
