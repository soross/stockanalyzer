using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer.Statistics.Weekly
{
    /// <summary>
    /// Categorize the stock data of each week, and display them to a logger
    /// </summary>
    class WeeklyStatistics
    {
        /// <summary>
        /// Add stock data from a stock history
        /// </summary>
        /// <param name="hist">a stock history</param>
        public void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);

                results_.AddStockData(stock);

                //startDate = DateFunc.GetNextWorkday(startDate);
                startDate = startDate.AddDays(1);
            }
        }

        /// <summary>
        /// Print results to a logger
        /// </summary>
        /// <param name="log">a logger</param>
        public void PrintResult(ICustomLog log)
        {
            results_.CalcResult(log);

            results_.AnalyzeEachDay(log);
        }

        WeeklyResults results_ = new WeeklyResults();
    }
}
