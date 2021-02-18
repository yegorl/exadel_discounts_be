using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Common.Models.Statistic;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    /// <summary>
    /// Represents interfaces for IStatisticRepository.
    /// </summary>
    public interface IStatisticRepository
    {
        Task<DiscountsStatistic> GetDiscountsStatistic(DiscountsStatisticCriteria criteria);
        Task<UsersStatistic> GetUsersStatistic();
    }
}
