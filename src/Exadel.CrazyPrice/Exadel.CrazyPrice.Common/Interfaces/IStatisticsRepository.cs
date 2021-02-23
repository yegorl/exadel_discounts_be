using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Common.Models.Statistics;
using System.Threading.Tasks;

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
