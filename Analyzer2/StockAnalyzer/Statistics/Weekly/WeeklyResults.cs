using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using System.Globalization;

namespace FinanceAnalyzer.Statistics.Weekly
{
    class WeeklyResults
    {
        public void AddStockData(IStockData dt)
        {
            int week = calendar_.GetWeekOfYear(dt.TradeDate, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            if (Results_.ContainsKey(week))
            {
                Results_[week].AddStockData(dt);
            }
            else
            {
                WeeklyResult res = new WeeklyResult();
                res.AddStockData(dt);

                Results_.Add(week, res);
            }
        }

        Dictionary<int, WeeklyResult> Results_ = new Dictionary<int, WeeklyResult>();

        Calendar calendar_ = CultureInfo.CurrentCulture.Calendar;
    }
}
