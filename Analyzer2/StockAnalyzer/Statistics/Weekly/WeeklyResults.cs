using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using System.Globalization;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Statistics.Weekly
{
    /// <summary>
    /// Categorize the stock data of each week, and display them to a logger
    /// </summary>
    class WeeklyResults
    {
        /// <summary>
        /// Add each stock data of a stock history
        /// </summary>
        /// <param name="dt">Stock data of one day</param>
        public void AddStockData(IStockData dt)
        {
            if (dt == null)
            {
                return;
            }

            DateTime localDate = DateFunc.ConvertToLocal(dt.TradeDate);
            int week = calendar_.GetWeekOfYear(localDate, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            int weekId = MakeWeekId(localDate.Year, week);

            if (Results_.ContainsKey(weekId))
            {
                Results_[weekId].AddStockData(dt);
            }
            else
            {
                WeeklyResult res = new WeeklyResult();
                res.AddStockData(dt);

                Results_.Add(weekId, res);
            }
        }

        /// <summary>
        /// Categorize the stock data
        /// </summary>
        /// <param name="log">log for output results</param>
        public void CalcResult(ICustomLog log)
        {
            int notCalcWeeks = 0;
            Dictionary<string, int> templateNumbers = new Dictionary<string, int>();
            foreach (WeeklyResult res in Results_.Values)
            {
                string s = res.CalcUpTemplate();

                if (string.IsNullOrEmpty(s))
                {
                    notCalcWeeks++;
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

            var keys = templateNumbers.Keys;
            var query = keys.OrderBy(x => x);

            foreach (string item in query)
            {
                log.LogInfo(item + ": " + templateNumbers[item]);
            }

            //foreach (var item in templateNumbers)
            //{
            //    log.LogInfo(item.Key + ": " + item.Value);
            //}

            log.LogInfo("Complete. Total weeks = " + Results_.Count 
                + ", categorized types = " + templateNumbers.Count
                + ", error weeks = " + notCalcWeeks);
        }

        public void AnalyzeEachDay(ICustomLog log)
        {
            foreach (WeeklyResult res in Results_.Values)
            {
            }
        }

        static int MakeWeekId(int year, int weekinYear)
        {
            return (year * 100) + weekinYear;
        }

        /// <summary>
        /// Key is WeekId (Year & weekinYear)
        /// </summary>
        Dictionary<int, WeeklyResult> Results_ = new Dictionary<int, WeeklyResult>();

        Calendar calendar_ = CultureInfo.CurrentCulture.Calendar;
    }
}
