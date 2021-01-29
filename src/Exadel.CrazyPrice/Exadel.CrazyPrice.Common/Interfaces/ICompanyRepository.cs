using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface ICompanyRepository
    {
        /// <summary>
        /// Gets a company list by first chars of company name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<string>> GetCompanyNamesAsync(string name);
    }
}
