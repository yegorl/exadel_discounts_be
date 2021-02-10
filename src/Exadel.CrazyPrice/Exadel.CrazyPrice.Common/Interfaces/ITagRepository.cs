using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    /// <summary>
    /// Represents interface for ITagRepository.
    /// </summary>
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
