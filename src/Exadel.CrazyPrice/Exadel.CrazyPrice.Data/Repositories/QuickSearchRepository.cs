using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    /// <summary>
    /// Represents repositories for address, company, tag.
    /// </summary>
    public class QuickSearchRepository : IAddressRepository, ICompanyRepository, ITagRepository
    {
        private readonly IMongoCollection<DbDiscount> _discounts;
        private readonly IMongoCollection<DbTag> _tags;

        /// <summary>
        /// Creates repositories for address, company, tag.
        /// </summary>
        /// <param name="configuration"></param>
        public QuickSearchRepository(IDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);
            _discounts = db.GetCollection<DbDiscount>("Discounts");
            _tags = db.GetCollection<DbTag>("Tags");
        }

        /// <summary>
        /// Gets countries.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="languageOption"></param>
        /// <returns></returns>
        public async Task<List<string>> GetCountriesAsync(string searchValue, LanguageOption languageOption)
        {
            if (searchValue == string.Empty)
            {
                return await GetCountriesAsync(languageOption);
            }

            var queryCompile = new Dictionary<string, string>
            {
                { "searchValue", searchValue.GetValidContent(CharOptions.Letter, " -") },
                { "field", "address.country" },
                { "query", "{ \"%field%\" : /^%searchValue%.*/i }"}
            }.CompileQuery();

            var list = new List<string>();

            Func<DbDiscount, bool> isIgnoredField = LanguageOption.En == languageOption
                ? l =>
                {
                    var field = l.Translations.Where(t => t.Language == "english").Select(t => t.Address.Country)
                        .FirstOrDefault();

                    return field == null || list.Contains(field);
                }
            : l => l.Address.Country == null || list.Contains(l.Address.Country);

            Action<DbDiscount> addToList = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
                ? l => list.Add(l.Translations.Where(t => t.Language == "english").Select(t => t.Address.Country).First())
                : l => list.Add(l.Address.Country);

            await GetListFromDiscountsAsync(queryCompile["query"], isIgnoredField, addToList);

            return list;
        }

        /// <summary>
        /// Gets cities by country.
        /// </summary>
        /// <param name="searchCountry"></param>
        /// <param name="searchValue"></param>
        /// <param name="languageOption"></param>
        /// <returns></returns>
        public async Task<List<string>> GetCitiesAsync(string searchCountry, string searchValue, LanguageOption languageOption)
        {
            if (searchValue == string.Empty)
            {
                return await GetCitiesAsync(searchCountry, languageOption);
            }

            var queryCompile = new Dictionary<string, string>
            {
                { "searchValue", searchValue.GetValidContent(CharOptions.Letter, " -") },
                { "searchCountry", searchCountry.GetValidContent(CharOptions.Letter, " -") },
                { "fieldCountry", "address.country" },
                { "fieldCity", "address.city" },
                { "field", "address.country" },
                { "query", "{$and : [ {\"%fieldCountry%\" : \"%searchCountry%\" }, {\"%fieldCity%\" : /^%searchValue%.*/i } ]}"}
            }.CompileQuery();

            var list = new List<string>();

            Func<DbDiscount, bool> isIgnoredField = LanguageOption.En == languageOption
                ? l =>
                {
                    var field = l.Translations.Where(t => t.Language == "english").Select(t => t.Address.City)
                        .FirstOrDefault();

                    return field == null || list.Contains(field);
                }
            : l => l.Address.City == null || list.Contains(l.Address.City);

            Action<DbDiscount> addToList = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
                ? l => list.Add(l.Translations.Where(t => t.Language == "english").Select(t => t.Address.City).First())
                : l => list.Add(l.Address.City);

            await GetListFromDiscountsAsync(queryCompile["query"], isIgnoredField, addToList);

            return list;
        }

        /// <summary>
        /// Gets companies.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public async Task<List<string>> GetCompanyNamesAsync(string searchValue)
        {
            var queryCompile = new Dictionary<string, string>
            {
                { "searchValue", searchValue.GetValidContent(CharOptions.Letter, " -") },
                { "field", "company.name" },
                { "query", "{ \"%field%\" : /^%searchValue%.*/i }"}
            }.CompileQuery();

            var list = new List<string>();

            Func<DbDiscount, bool> isIgnoredField = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
                ? l =>
                {
                    var field = l.Translations.Where(t => t.Language == "english").Select(t => t.Company.Name)
                        .FirstOrDefault();

                    return field == null || list.Contains(field);
                }
            : l => l.Company.Name == null || list.Contains(l.Company.Name);

            Action<DbDiscount> addToList = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
                ? l => list.Add(l.Translations.Where(t => t.Language == "english").Select(t => t.Company.Name).First())
                : l => list.Add(l.Company.Name);

            await GetListFromDiscountsAsync(queryCompile["query"], isIgnoredField, addToList);

            return list;
        }

        /// <summary>
        /// Gets tags.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public async Task<List<string>> GetTagAsync(string searchValue)
        {
            var queryCompile = new Dictionary<string, string>
            {
                { "searchValue", searchValue.GetValidContent(CharOptions.Letter, " -") },
                { "field", "_id" },
                { "query", "{ \"%field%\" : /^%searchValue%.*/i }"}
            }.CompileQuery();

            var list = new List<string>();

            Func<DbTag, bool> isIgnoredField = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
                ? l =>
                {
                    var field = l.Translations.Where(t => t.Language == "english").Select(t => t.Name)
                        .FirstOrDefault();

                    return field == null || list.Contains(field);
                }
            : l => l.Name == null || list.Contains(l.Name);

            Action<DbTag> addToList = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
                ? l => list.Add(l.Translations.Where(t => t.Language == "english").Select(t => t.Name).First())
                : l => list.Add(l.Name);

            await GetListFromTagsAsync(queryCompile["query"], isIgnoredField, addToList);

            return list;
        }

        private async Task GetListFromDiscountsAsync(string query, Func<DbDiscount, bool> isIgnoredField, Action<DbDiscount> addToList)
        {
            var i = 1;
            using var cursor = await _discounts.FindAsync(query, new FindOptions<DbDiscount> { Limit = 10000 });
            while (await cursor.MoveNextAsync())
            {
                var items = cursor.Current;
                foreach (var item in items)
                {
                    if (isIgnoredField(item))
                    {
                        continue;
                    }

                    addToList(item);
                    i++;
                    if (i > 7)
                    {
                        return;
                    }
                }
            }
        }

        private async Task GetListFromTagsAsync(string query, Func<DbTag, bool> isIgnoredField, Action<DbTag> addToList)
        {
            var i = 1;
            using var cursor = await _tags.FindAsync(query, new FindOptions<DbTag> { Limit = 10000 });
            while (await cursor.MoveNextAsync())
            {
                var items = cursor.Current;
                foreach (var item in items)
                {
                    if (isIgnoredField(item))
                    {
                        continue;
                    }

                    addToList(item);
                    i++;
                    if (i > 7)
                    {
                        return;
                    }
                }
            }
        }

        private async Task<List<string>> GetCountriesAsync(LanguageOption languageOption) =>
            await _discounts.Distinct<string>(
                "address.country".GetWithTranslationsPrefix(languageOption),
                "{ \"" + "language".GetWithTranslationsPrefix(languageOption) + "\" : \"" + languageOption.ToStringLookup() + "\" }").ToListAsync();

        private async Task<List<string>> GetCitiesAsync(string searchCountry, LanguageOption languageOption)
        {
            return await _discounts.Distinct<string>(
                "address.city".GetWithTranslationsPrefix(languageOption),
                "{$and : [{\"" + "address.country".GetWithTranslationsPrefix(languageOption)
                              + "\" : \"" + searchCountry + "\"}, { \""
                              + "language".GetWithTranslationsPrefix(languageOption) + "\" : \""
                              + languageOption.ToStringLookup() + "\" }]}").ToListAsync();
        }
    }
}
