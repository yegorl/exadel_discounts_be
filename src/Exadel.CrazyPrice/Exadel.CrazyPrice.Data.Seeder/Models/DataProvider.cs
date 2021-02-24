using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models
{
    public abstract class DataProvider : IDataProvider
    {
        protected List<ISeed> Collections;
        protected string Status;

        private readonly Stopwatch _watch = new();
        private bool _completed;
        private DateTime _startDateTime;
        private DateTime _endDateTime;

        public async Task WriteAsync(bool showReport = true)
        {
            _completed = false;
            StartDateTime = DateTime.Now;
            _watch.Start();
            await Work();
            _watch.Stop();
            EndDateTime = DateTime.Now;
            _completed = true;

            if (showReport)
            {
                Report();
            }
        }

        /// <summary>
        /// Seeds fake data.
        /// </summary>
        /// <returns></returns>
#pragma warning disable 1998
        protected virtual async Task Work()
#pragma warning restore 1998
        { }

        public DateTime StartDateTime
        {
            get => IsCompleted(nameof(StartDateTime)) ? _startDateTime : default;
            private set => _startDateTime = value;
        }

        public DateTime EndDateTime
        {
            get => IsCompleted(nameof(EndDateTime)) ? _endDateTime : default;
            private set => _endDateTime = value;
        }

        public double ExecutionTime => IsCompleted(nameof(ExecutionTime)) ? _watch.Elapsed.TotalSeconds : default;

        public Action ActionWhenAborted { get; set; }

        public virtual void Report()
        {
            IsCompleted("Report()");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Finished." + Environment.NewLine +
                              Status +
                              $"Start time: {StartDateTime}" + Environment.NewLine +
                              $"End time: {EndDateTime}" + Environment.NewLine +
                              $"TotalSeconds: {ExecutionTime:0.000000}");
        }

        private bool IsCompleted(string name)
        {
            if (!_completed)
            {
                throw new ArgumentException($"Cannot access to '{name}'. The work is not completed.");
            }

            return true;
        }

    }
}