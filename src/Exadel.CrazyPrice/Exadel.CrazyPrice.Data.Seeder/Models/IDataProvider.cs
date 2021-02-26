using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models
{
    /// <summary>
    /// Represents data provider for fake data.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Writes async fake data. Show the Report after seed when showReport is true.
        /// </summary>
        /// <param name="showReport"></param>
        /// <returns></returns>
        Task WriteAsync(bool showReport = true);

        /// <summary>
        /// Gets the DateTime before seed.
        /// </summary>
        DateTime StartDateTime { get; }

        /// <summary>
        /// Gets the DateTime after seed.
        /// </summary>
        DateTime EndDateTime { get; }

        /// <summary>
        /// Gets Execution Time.
        /// </summary>
        double ExecutionTime { get; }

        /// <summary>
        /// Gets the Report after execution.
        /// </summary>
        void Report();
        
        /// <summary>
        /// Gets Action for execution after aborted.
        /// </summary>
        Action ActionWhenAborted { get; set; }
    }
}