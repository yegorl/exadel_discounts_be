using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface ITagRepository
    {
        /// <summary>
        /// Gets a tag list by first chars of tag name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<string>> GetTagAsync(string name);
    }
}
