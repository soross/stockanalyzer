using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Statistics.Weekly
{
    /// <summary>
    /// Categorize the stock data of one week
    /// </summary>
    class WeeklyResult
    {
        /// <summary>
        /// Add each stock data of a stock history
        /// </summary>
        /// <param name="dt">Stock data of one day</param>
        public void AddStockData(IStockData dt)
        {
            DayOfWeek dw = DateFunc.ConvertToLocal(dt.TradeDate).DayOfWeek;

            if (dayToStocks_.ContainsKey(dw))
            {
                // Do nothing
            }
            else
            {
                dayToStocks_.Add(dw, dt);
            }
        }

        /// <summary>
        /// Get stock information on specific day of one week
        /// </summary>
        /// <param name="dw">Day of week</param>
        /// <returns>Stock data</returns>
        public IStockData GetStockData(DayOfWeek dw)
        {
            if (dayToStocks_.ContainsKey(dw))
            {
                return dayToStocks_[dw];
            }
            else
            {
                return null;
            }
        }

        public bool IsWholeWeek()
        {
            return dayToStocks_.ContainsKey(DayOfWeek.Monday)
                && dayToStocks_.ContainsKey(DayOfWeek.Tuesday)
                && dayToStocks_.ContainsKey(DayOfWeek.Wednesday)
                && dayToStocks_.ContainsKey(DayOfWeek.Thursday)
                && dayToStocks_.ContainsKey(DayOfWeek.Friday);
        }

        public string CalcUpTemplate()
        {
            if (!IsWholeWeek())
            {
                return "";
            }

            string s = "";

            IStockData stock1 = dayToStocks_[DayOfWeek.Monday];
            s += GetUpDescription(stock1);
            IStockData stock2 = dayToStocks_[DayOfWeek.Tuesday];
            s += GetUpDescription(stock2);
            IStockData stock3 = dayToStocks_[DayOfWeek.Wednesday];
            s += GetUpDescription(stock3);
            IStockData stock4 = dayToStocks_[DayOfWeek.Thursday];
            s += GetUpDescription(stock4);
            IStockData stock5 = dayToStocks_[DayOfWeek.Friday];
            s += GetUpDescription(stock5);

            return s;
        }

        string GetUpDescription(IStockData sd)
        {
            if (sd.EndPrice > sd.StartPrice)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        Dictionary<DayOfWeek, IStockData> dayToStocks_ = new Dictionary<DayOfWeek, IStockData>();
    }
}
