using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
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
            discount.Address = translation.Address.Clone(discount);
            discount.Company = translation.Company.Clone(discount);
            discount.Tags = translation.Tags;

            discount.Translations = null;

            return discount;
        }

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
                Translations = upsertDiscountRequest.Translations
            };
        }

        /// <summary>
        /// Gets UpsertDiscountRequest entity from Discount entity.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static UpsertDiscountRequest ToUpsertDiscountRequest(this Discount discount)
        {
            return discount.IsEmpty()
                ? null
                : new UpsertDiscountRequest
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
                    Translations = discount.Translations
                };
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
    }
}
