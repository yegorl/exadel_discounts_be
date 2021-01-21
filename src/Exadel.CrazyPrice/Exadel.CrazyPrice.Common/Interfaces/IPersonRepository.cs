using System;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Gets a Person by id.
        /// </summary>
        /// <param name="id">The id Person</param>
        /// <returns></returns>
        Task<Person> GetPersonAsync(Guid id);
    }
}
