using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Result
{
    /// <summary>
    /// Compare all strategies results
    /// </summary>
    public interface IStrategyResults
    {
        /// <summary>
        /// Get strategy trade results of each day
        /// </summary>
        /// <param name="strategyName">strategy name</param>
        /// <returns>strategy performance</returns>
        IStockValues GetResult(string strategyName);

        /// <summary>
        /// Return all compared strategies
        /// </summary>
        ICollection<string> AllStrategyNames
        {
            get;
        }
    }
}
