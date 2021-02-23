using Exadel.CrazyPrice.Common.Models.Option;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    /// <summary>
    /// Represents interfaces for IAddressRepository.
    /// </summary>
    public interface IAddressRepository
    {
        /// <summary>
        /// Gets countries by string.
        /// </summary>
        /// <param name="searchValue">The part name country.</param>
        /// <param name="languageOption"></param>
        /// <returns></returns>
        Task<List<string>> GetCountriesAsync(string searchValue, LanguageOption languageOption);

        /// <summary>
        /// Gets cities from string by country.
        /// </summary>
        /// <param name="searchCountry">The country name.</param>
        /// <param name="searchCity">The part name city.</param>
        /// <param name="languageOption"></param>
        /// <returns></returns>
        Task<List<string>> GetCitiesAsync(string searchCountry, string searchCity, LanguageOption languageOption);
    }
}
