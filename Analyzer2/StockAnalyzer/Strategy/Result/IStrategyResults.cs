using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Strategy.Result
{
    interface IStrategyResults
    {
        IStockValues GetResult(string strategyName);

        ICollection<string> AllStrategyNames
        {
            get;
        }
    }
}
