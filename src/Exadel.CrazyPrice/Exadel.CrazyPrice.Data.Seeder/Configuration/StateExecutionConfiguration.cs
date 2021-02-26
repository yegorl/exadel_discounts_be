using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Seeder.Configuration
{
    public class StateExecutionConfiguration
    {
        private bool _aborted = true;

        public void Success() => _aborted = false;

        public bool Aborted => _aborted;

        public Action ExecutionActionsAfterAbort;
    }
}
