using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    /// <summary>
    /// Calculate all buy and sell signal, and save
    /// </summary>
    abstract class BasicIndicatorCalc : IIndicatorCalc
    {
        public abstract void Calc(IStockHistory hist);

        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// Return the buy or sell signal based on the indicator value
        /// </summary>
        /// <param name="dt">Current date</param>
        /// <param name="prev">Previous date</param>
        /// <returns>buy or sell signal</returns>
        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (DateToOpers_.ContainsKey(dt))
            {
                return DateToOpers_[dt];
            }

            return OperType.NoOper;
        }

        protected Dictionary<DateTime, OperType> DateToOpers_ = new Dictionary<DateTime, OperType>();
    }
}
