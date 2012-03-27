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

        public void CalcResult()
        {
            Dictionary<string, int> templateNumbers = new Dictionary<string, int>();
            foreach (WeeklyResult res in Results_.Values)
            {
                string s = res.CalcUpTemplate();

                if (string.IsNullOrEmpty(s))
                {
                    continue;
                }

                if (templateNumbers.ContainsKey(s))
                {
                    templateNumbers[s] = templateNumbers[s] + 1;
                }
                else
                {
                    templateNumbers.Add(s, 1);
                }
            }

            foreach (var item in templateNumbers)
            {
                System.Console.WriteLine(item.Key + ": " + item.Value);
            }
        }

        Dictionary<int, WeeklyResult> Results_ = new Dictionary<int, WeeklyResult>();

        Calendar calendar_ = CultureInfo.CurrentCulture.Calendar;
    }
}
