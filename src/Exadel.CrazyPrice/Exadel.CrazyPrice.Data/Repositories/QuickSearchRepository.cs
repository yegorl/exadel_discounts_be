using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbTag = Exadel.CrazyPrice.Data.Models.DbTag;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class QuickSearchRepository : IAddressRepository, ICompanyRepository, IPersonRepository, ITagRepository
    {
        private readonly IMongoCollection<DbDiscount> _discounts;
        private readonly IMongoCollection<DbUser> _users;
        private readonly IMongoCollection<DbTag> _tags;

        public QuickSearchRepository(IOptions<MongoDbConfiguration> configuration)
        {
            var client = new MongoClient(configuration.Value.ConnectionString);
            var db = client.GetDatabase(configuration.Value.Database);
            _discounts = db.GetCollection<DbDiscount>("Discounts");
            _users = db.GetCollection<DbUser>("Users");
            _tags = db.GetCollection<DbTag>("Tags");
        }

        public async Task<List<string>> GetCountriesAsync(string searchValue)
        {
            var queryCompile = new Dictionary<string, string>
            {
                { "searchValue", searchValue.GetValidContent(CharOptions.Letter, " -") },
                { "field", "address.country" },
                { "query", "{ \"%field%\" : /^%searchValue%.*/i }"}
            }.CompileQuery();

            var list = new List<string>();

            Func<DbDiscount, bool> isIgnoredField = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
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

        public async Task<List<string>> GetCitiesAsync(string searchCountry, string searchValue)
        {
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

            Func<DbDiscount, bool> isIgnoredField = LanguageOption.En == queryCompile["searchValue"].GetLanguageFromFirstLetter()
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
        
        public async Task<Person> GetPersonByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
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
    }
}
