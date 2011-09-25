using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator
{
    abstract class HistoricalValuesCalc : IIndicatorCalc
    {
        public abstract void Calc(IStockHistory hist);

        public abstract string Name
        {
            get;
        }

        public abstract OperType MatchSignal(DateTime dt, DateTime prev);

        // 得到某天的指标值
        public double GetIndicatorValue(DateTime dt)
        {
            if (_DateIndicators.ContainsKey(dt))
            {
                return _DateIndicators[dt];
            }
            else
            {
                return double.NaN; // 默认返回NaN 
            }
        }

        protected Dictionary<DateTime, double> _DateIndicators = new Dictionary<DateTime, double>();
    }
}
