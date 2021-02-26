using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exadel.CrazyPrice.Common.Extentions
{
    /// <summary>
    /// Represents extentions for Discount.
    /// </summary>
    public static class DiscountExtentions
    {
        /// <summary>
        /// Translates the discount to target language.
        /// </summary>
        /// <param name="discount"></param>
        /// <param name="languageOption"></param>
        /// <returns></returns>
        public static Discount Translate(this Discount discount, LanguageOption languageOption)
        {
            if (discount.IsEmpty())
            {
                return null;
            }

            var translation = discount.Translations?.FirstOrDefault(t => t.Language == languageOption);
            if (translation == null)
            {
                discount.Translations = null;
                return discount;
            }

            discount.Name = translation.Name;
            discount.Description = translation.Description;

            discount.Address.Country = translation.Address.Country;
            discount.Address.City = translation.Address.City;
            discount.Address.Street = translation.Address.Street;

            discount.Company.Name = translation.Company.Name;
            discount.Company.Description = translation.Company.Description;

            discount.Tags = translation.Tags;

            discount.Translations = null;

            return discount;
        }

        /// <summary>
        /// Gets Discount entity from UpsertDiscountRequest entity.
        /// </summary>
        /// <param name="upsertDiscountRequest"></param>
        /// <returns></returns>
        public static Discount ToDiscount(this UpsertDiscountRequest upsertDiscountRequest)
        {
            return new()
            {
                Id = upsertDiscountRequest.Id,
                Name = upsertDiscountRequest.Name,
                Description = upsertDiscountRequest.Description,
                AmountOfDiscount = upsertDiscountRequest.AmountOfDiscount,
                StartDate = upsertDiscountRequest.StartDate,
                EndDate = upsertDiscountRequest.EndDate,
                Address = upsertDiscountRequest.Address,
                Company = upsertDiscountRequest.Company,
                WorkingDaysOfTheWeek = upsertDiscountRequest.WorkingDaysOfTheWeek,
                Tags = upsertDiscountRequest.Tags,
                PictureUrl = upsertDiscountRequest.PictureUrl,
                Language = upsertDiscountRequest.Language,
                Translations = upsertDiscountRequest.Translations,
                PromocodeOptions = upsertDiscountRequest.PromocodeOptions
            };
        }

        /// <summary>
        /// Gets UpsertDiscountRequest entity from Discount entity.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static UpsertDiscountRequest ToUpsertDiscountRequest(this Discount discount)
        {
            if (discount.IsEmpty())
            {
                return null;
            }

            var upsert = new UpsertDiscountRequest
            {
                Id = discount.Id,
                Name = discount.Name,
                Description = discount.Description,
                AmountOfDiscount = discount.AmountOfDiscount,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                Address = discount.Address,
                Company = discount.Company,
                WorkingDaysOfTheWeek = discount.WorkingDaysOfTheWeek,
                Tags = discount.Tags,
                PictureUrl = discount.PictureUrl,
                Language = discount.Language,
                Translations = discount.Translations,
                PromocodeOptions = discount.PromocodeOptions
            };

            return upsert;
        }

        /// <summary>
        /// Gets DiscountResponse entity from Discount entity.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static DiscountResponse ToDiscountResponse(this Discount discount)
        {
            return discount.IsEmpty()
                ? null
                : new DiscountResponse
                {
                    Id = discount.Id,
                    Name = discount.Name,
                    Description = discount.Description,
                    AmountOfDiscount = discount.AmountOfDiscount,
                    StartDate = discount.StartDate,
                    EndDate = discount.EndDate,
                    Address = discount.Address,
                    Company = discount.Company,
                    WorkingDaysOfTheWeek = discount.WorkingDaysOfTheWeek,
                    Tags = discount.Tags,
                    PictureUrl = discount.PictureUrl,

                    RatingTotal = discount.RatingTotal,
                    ViewsTotal = discount.ViewsTotal,
                    SubscriptionsTotal = discount.SubscriptionsTotal,
                    UsersSubscriptionTotal = discount.UsersSubscriptionTotal,
                    UserPromocodes = discount.UsersPromocodes,

                    CreateDate = discount.CreateDate,
                    LastChangeDate = discount.LastChangeDate,

                    UserCreateDate = discount.UserCreateDate,
                    UserLastChangeDate = discount.UserLastChangeDate,

                    Deleted = discount.Deleted
                };
        }

        /// <summary>
        /// Gets the list DiscountResponse entity from Discount entity.
        /// </summary>
        /// <param name="discounts"></param>
        /// <param name="languageOption"></param>
        /// <returns></returns>
        public static List<DiscountResponse> ToListDiscountResponse(this List<Discount> discounts, LanguageOption languageOption)
        {
            return discounts == null || discounts.Count == 0
                ? null
                : discounts.Select(d => d.Translate(languageOption).ToDiscountResponse()).ToList();
        }

        /// <summary>
        /// Added time create and last change of discount by user.
        /// </summary>
        /// <param name="discount"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Discount AddChangeUserTime(this Discount discount, User user)
        {
            discount.CreateDate = DateTime.Now;
            discount.LastChangeDate = discount.CreateDate;

            discount.UserCreateDate = user;
            discount.UserLastChangeDate = discount.UserCreateDate;

            return discount;
        }

        /// <summary>
        /// Gets the discount filtered with role.
        /// </summary>
        /// <param name="discount"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Discount TransformUsersPromocodes(this Discount discount, IncomingUser user)
        {
            if (discount.IsEmpty())
            {
                return discount;
            }

            discount.UsersPromocodes = discount.UsersPromocodes?.Where(i => i.UserId == user.Id).ToList();
            return discount;
        }

        /// <summary>
        /// Gets the discounts filtered with role.
        /// </summary>
        /// <param name="discounts"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Discount> TransformUsersPromocodes(this List<Discount> discounts, IncomingUser user) =>
            discounts.Select(discount => discount.TransformUsersPromocodes(user)).ToList();

        /// <summary>
        /// Gets true when the DiscountResponse entity or id property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Discount discount)
        {
            return discount == null || discount.Id == Guid.Empty;
        }

        /// <summary>
        /// Gets true when the PromocodeOptions entity or all property is Null otherwise false.
        /// </summary>
        /// <param name="promocodeOptions"></param>
        /// <returns></returns>
        public static bool IsEmpty(this PromocodeOptions promocodeOptions) =>
            promocodeOptions == null ||
            (promocodeOptions.CountSymbolsPromocode == null
             && promocodeOptions.TimeLimitAddingInSeconds == null
             && promocodeOptions.CountActivePromocodePerUser == null
             && promocodeOptions.DaysDurationPromocode == null);

        /// <summary>
        /// Gets true when the Address entity or all property is Null otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Address value) =>
            value == null ||
            (value.City.IsNullOrEmpty()
             && value.Country.IsNullOrEmpty()
             && value.Street.IsNullOrEmpty()
             && value.Location == null
            );

        /// <summary>
        /// Gets true when the Location entity or all property is Null or 0 otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Location value) =>
            value == null ||
            (value.Longitude == 0
             && value.Latitude == 0
            );

        /// <summary>
        /// Gets true when the Company entity or all property is Null otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Company value) =>
            value == null ||
            (value.Description.IsNullOrEmpty()
             && value.Mail.IsNullOrEmpty()
             && value.Name.IsNullOrEmpty()
             && value.PhoneNumber.IsNullOrEmpty()
            );

        /// <summary>
        /// Gets true when the List Translation entity is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this List<Translation> value) =>
            value == null || value.Count == 0;

        /// <summary>
        /// Gets true when the Promocode entity or id property is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Promocode value) =>
            value == null || value.Id == Guid.Empty;

        /// <summary>
        /// Gets true when the List UserPromocodes entity is Null or Empty otherwise false.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsEmpty(this List<UserPromocodes> values) =>
            values == null || values.Count == 0;

        /// <summary>
        /// Gets true when the UserPromocodes entity or user id property is Null or Empty or Promocodes is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this UserPromocodes value) =>
            value == null || value.UserId == Guid.Empty || value.Promocodes == null || value.Promocodes.Count == 0;
    }
}
