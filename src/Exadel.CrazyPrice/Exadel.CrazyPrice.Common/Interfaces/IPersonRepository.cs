using System;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Gets a Person by uid.
        /// </summary>
        /// <param name="uid">The uid Person</param>
        /// <returns></returns>
        Task<Person> GetPersonByUidAsync(Guid uid);
    }
}
