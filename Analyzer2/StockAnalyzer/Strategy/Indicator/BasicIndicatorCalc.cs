using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator
{
    abstract class BasicIndicatorCalc : IIndicatorCalc
    {
        public abstract void Calc(IStockHistory hist);

        public abstract string Name
        {
            get;
        }

        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (_DateToOpers.ContainsKey(dt))
            {
                return _DateToOpers[dt];
            }

            return OperType.NoOper;
        }

        protected Dictionary<DateTime, OperType> _DateToOpers = new Dictionary<DateTime, OperType>();
    }
}
