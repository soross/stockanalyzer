using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Statistics.Weekly
{
    class WeeklyResult
    {
        public void AddStockData(IStockData dt)
        {
            DayOfWeek dw = dt.TradeDate.DayOfWeek;

            if (dayToStocks_.ContainsKey(dw))
            {
                // Do nothing
            }
            else
            {
                dayToStocks_.Add(dw, dt);
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

        public string CalcTemplate()
        {
            if (!IsWholeWeek())
            {
                return "";
            }



            return "";
        }

        Dictionary<DayOfWeek, IStockData> dayToStocks_ = new Dictionary<DayOfWeek, IStockData>();
    }
}
