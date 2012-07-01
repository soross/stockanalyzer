using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Result
{
    /// <summary>
    /// Save the strategy name and the total value of each day
    /// </summary>
    public class StrategyResults : IStrategyResults
    {
        public void AddResult(string strategyName, IStockValues vals)
        {
            NameToValues_.Add(strategyName, vals);
        }

        public IStockValues GetResult(string strategyName)
        {
            IStockValues values = null;
            if (!NameToValues_.TryGetValue(strategyName, out values))
            {
                return null;
            }
            else
            {
                return values;
            }
        }

        public ICollection<string> AllStrategyNames
        {
            get
            {
                return NameToValues_.Keys;
            }
        }

        private SortedDictionary<string, IStockValues> NameToValues_ = new SortedDictionary<string, IStockValues>();
    }
}
