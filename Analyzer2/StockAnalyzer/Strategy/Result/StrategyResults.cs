using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Result
{
    // 保存策略名称以及对应的结果 
    public class StrategyResults : IStrategyResults
    {
        public void AddResult(string strategyName, IStockValues vals)
        {
            _NameToValues.Add(strategyName, vals);
        }

        public IStockValues GetResult(string strategyName)
        {
            IStockValues values = null;
            if (!_NameToValues.TryGetValue(strategyName, out values))
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
                return _NameToValues.Keys;
            }
        }

        private SortedDictionary<string, IStockValues> _NameToValues = new SortedDictionary<string, IStockValues>();
    }
}
