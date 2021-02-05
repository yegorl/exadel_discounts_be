using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface IAddressRepository
    {
        /// <summary>
        /// Gets countries by string.
        /// </summary>
        /// <param name="searchValue">The part name country.</param>
        /// <returns></returns>
        Task<List<string>> GetCountriesAsync(string searchValue);

        /// <summary>
        /// Gets cities from string by country.
        /// </summary>
        /// <param name="searchCountry">The country name.</param>
        /// <param name="searchCity">The part name city.</param>
        /// <returns></returns>
        Task<List<string>> GetCitiesAsync(string searchCountry, string searchCity);
    }
}
