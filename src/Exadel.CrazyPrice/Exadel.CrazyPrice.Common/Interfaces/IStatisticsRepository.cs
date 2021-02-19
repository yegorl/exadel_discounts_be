using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Statistics;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    /// <summary>
    /// Represents interfaces for IStatisticsRepository.
    /// </summary>
    public interface IStatisticsRepository
    {
        Task<DiscountsStatistics> GetDiscountsStatistics(DiscountsStatisticsCriteria criteria);
        
        Task<UsersStatistics> GetUsersStatistics();
    }
}
