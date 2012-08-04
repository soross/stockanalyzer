using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Strategy
{
    public interface IStrategyFactory
    {
        IFinanceStrategy GetStrategy(string strategyName);

        ICollection<IFinanceStrategy> AllStrategies
        {
            get;
        }

        ICollection<string> AllStrategyNames
        {
            get;
        }

        void Remove(string strategyName);
    }
}
