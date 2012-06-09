using System;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    /// <summary>
    /// Buy and sell strategy based on some indicators
    /// </summary>
    public interface IIndicatorCalc
    {
        /// <summary>
        /// Calculate the indicator value using the stock history
        /// </summary>
        /// <param name="hist">the stock history</param>
        void Calc(IStockHistory hist);

        /// <summary>
        /// Return the buy or sell signal based on the indicator value
        /// </summary>
        /// <param name="dt">Current date</param>
        /// <param name="prev">Previous date</param>
        /// <returns>buy or sell signal</returns>
        OperType MatchSignal(DateTime dt, DateTime prev);

        /// <summary>
        /// Strategy name
        /// </summary>
        string Name
        {
            get;
        }
    }
}
