using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Data.Models;
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
        /// <param name="dbDiscounts"></param>
        /// <returns></returns>
        public static DbDiscount GetOne(this List<DbDiscount> dbDiscounts)
        {
            return dbDiscounts == null || dbDiscounts.Count == 0 ? new DbDiscount() : dbDiscounts[0];
        }

        /// <summary>
        /// Gets true when the DbDiscount entity or id property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="dbDiscount"></param>
        /// <returns></returns>
        public static bool IsEmpty(this DbDiscount dbDiscount)
        {
            return dbDiscount == null || string.IsNullOrEmpty(dbDiscount.Id);
        }

        /// <summary>
        /// Gets Discount entity from DbDiscount entity.
        /// </summary>
        /// <param name="dbDiscount"></param>
        /// <returns></returns>
        public static Discount ToDiscount(this DbDiscount dbDiscount)
        {
            if (dbDiscount.IsEmpty())
            {
                return new Discount();
            }

            try
            {
                return new Discount
                {
                    Id = new Guid(dbDiscount.Id),
                    Name = dbDiscount.Name,
                    Description = dbDiscount.Description,
                    AmountOfDiscount = dbDiscount.AmountOfDiscount,
                    StartDate = dbDiscount.StartDate,
                    EndDate = dbDiscount.EndDate,
                    Address = dbDiscount.Address.ToAddress(),
                    Company = dbDiscount.Company.ToCompany(),
                    WorkingDaysOfTheWeek = dbDiscount.WorkingDaysOfTheWeek,
                    Tags = dbDiscount.Tags,
                    Language = dbDiscount.Language.ToLanguageOption(),
                    Translations = dbDiscount.Translations.ToTranslations(),

                    RatingTotal = dbDiscount.RatingTotal,
                    ViewsTotal = dbDiscount.ViewsTotal,
                    SubscriptionsTotal = dbDiscount.SubscriptionsTotal,

                    FavoritesUsersId = dbDiscount.FavoritesUsersId,
                    SubscriptionsUsersId = dbDiscount.SubscriptionsUsersId,

                    CreateDate = dbDiscount.CreateDate,
                    LastChangeDate = dbDiscount.LastChangeDate,

                    UserCreateDate = dbDiscount.UserCreateDate.ToUser(),
                    UserLastChangeDate = dbDiscount.UserLastChangeDate.ToUser(),

                    Deleted = dbDiscount.Deleted
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
        /// <param name="discount"></param>
        /// <returns></returns>
        public static DbDiscount ToDbDiscount(this Discount discount)
        {
            return new()
            {
                Id = discount.Id.ToString(),
                Name = discount.Name,
                Description = discount.Description,
                AmountOfDiscount = discount.AmountOfDiscount,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                Address = discount.Address.ToDbAddress(),
                Company = discount.Company.ToDbCompany(),
                WorkingDaysOfTheWeek = discount.WorkingDaysOfTheWeek,
                Tags = discount.Tags,
                Language = discount.Language.ToStringLookup(),
                Translations = discount.Translations.ToDbTranslations(),

                RatingTotal = discount.RatingTotal,
                ViewsTotal = discount.ViewsTotal,
                SubscriptionsTotal = discount.SubscriptionsTotal,

                FavoritesUsersId = discount.FavoritesUsersId,
                SubscriptionsUsersId = discount.SubscriptionsUsersId,

                CreateDate = discount.CreateDate,
                LastChangeDate = discount.LastChangeDate,

                UserCreateDate = discount.UserCreateDate.ToDbUser(),
                UserLastChangeDate = discount.UserLastChangeDate.ToDbUser(),

                Deleted = discount.Deleted
            };
        }
        
        /// <summary>
        /// Gets Address entity from DbAddress entity.
        /// </summary>
        /// <param name="dbAddress"></param>
        /// <returns></returns>
        public static Address ToAddress(this DbAddress dbAddress)
        {
            return dbAddress == null ? null : new Address
            {
                Country = dbAddress.Country,
                City = dbAddress.City,
                Street = dbAddress.Street,
                Location = dbAddress.Location.ToLocation()
            };
        }

        /// <summary>
        /// Gets DbAddress entity from Address entity.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static DbAddress ToDbAddress(this Address address)
        {
            return address == null ? null : new DbAddress
            {
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                Location = address.Location.ToDbLocation()
            };
        }

        /// <summary>
        /// Gets Location entity from DbLocation entity.
        /// </summary>
        /// <param name="dbLocation"></param>
        /// <returns></returns>
        public static Location ToLocation(this DbLocation dbLocation)
        {
            return dbLocation == null ? null : new Location
            {
                Longitude = dbLocation.Longitude,
                Latitude = dbLocation.Latitude
            };
        }

        /// <summary>
        /// Gets DbLocation entity from Location entity.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static DbLocation ToDbLocation(this Location location)
        {
            return location == null ? null : new DbLocation
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

        /// <summary>
        /// Gets Company entity from DbCompany entity.
        /// </summary>
        /// <param name="dbCompany"></param>
        /// <returns></returns>
        public static Company ToCompany(this DbCompany dbCompany)
        {
            return dbCompany == null ? null : new Company
            {
                Name = dbCompany.Name,
                Description = dbCompany.Description,
                PhoneNumber = dbCompany.PhoneNumber,
                Mail = dbCompany.Mail
            };
        }
        
        /// <summary>
        /// Gets DbCompany entity from Company entity.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static DbCompany ToDbCompany(this Company company)
        {
            return company == null ? null : new DbCompany
            {
                Name = company.Name,
                Description = company.Description,
                PhoneNumber = company.PhoneNumber,
                Mail = company.Mail
            };
        }

        /// <summary>
        /// Gets List Translation from List DbTranslation.
        /// </summary>
        /// <param name="dbTranslations"></param>
        /// <returns></returns>
        public static List<Translation> ToTranslations(this List<DbTranslation> dbTranslations)
        {
            return dbTranslations?.Select(dbTranslation => new Translation
            {
                Language = dbTranslation.Language.ToLanguageOption(),
                Name = dbTranslation.Name,
                Tags = dbTranslation.Tags,
                Description = dbTranslation.Description,
                Address = dbTranslation.Address.ToAddress(),
                Company = dbTranslation.Company.ToCompany()
            })
                .ToList();
        }

        /// <summary>
        /// Gets List DbTranslation from List Translation.
        /// </summary>
        /// <param name="translations"></param>
        /// <returns></returns>
        public static List<DbTranslation> ToDbTranslations(this List<Translation> translations)
        {
            return translations?.Select(dbTranslation => new DbTranslation
            {
                Language = dbTranslation.Language.ToStringLookup(),
                Name = dbTranslation.Name,
                Tags = dbTranslation.Tags,
                Description = dbTranslation.Description,
                Address = dbTranslation.Address.ToDbAddress(),
                Company = dbTranslation.Company.ToDbCompany()
            })
                .ToList();
        }
    }
}
