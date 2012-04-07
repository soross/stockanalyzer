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
        /// <param name="log">logger for output results</param>
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
            
            log.LogInfo("Complete. Total weeks = " + Results_.Count 
                + ", categorized types = " + templateNumbers.Count
                + ", error weeks = " + notCalcWeeks);
        }

        /// <summary>
        /// Categorized stock performance by each day of one week
        /// </summary>
        /// <param name="log">logger for output results</param>
        public void AnalyzeEachDayOfOneWeek(ICustomLog log)
        {            
            StockWeekData mondaydata = new StockWeekData();
            StockWeekData tuesdaydata = new StockWeekData();
            StockWeekData wednesdaydata = new StockWeekData();
            StockWeekData thursdaydata = new StockWeekData();
            StockWeekData fridaydata = new StockWeekData();
            
            foreach (WeeklyResult res in Results_.Values)
            {
                IStockData sdMonday = res.GetStockData(DayOfWeek.Monday);
                IStockData sdTuesday = res.GetStockData(DayOfWeek.Tuesday);
                IStockData sdWednesday = res.GetStockData(DayOfWeek.Wednesday);
                IStockData sdThursday = res.GetStockData(DayOfWeek.Thursday);
                IStockData sdFriday = res.GetStockData(DayOfWeek.Friday);
                                
                mondaydata.AddStockData(sdMonday);
                tuesdaydata.AddStockData(sdTuesday);
                wednesdaydata.AddStockData(sdWednesday);
                thursdaydata.AddStockData(sdThursday);
                fridaydata.AddStockData(sdFriday);
            }

            log.LogInfo("Monday up percent: " + mondaydata.CalcUpPercent().ToString("F03")
                + ", Days = " + mondaydata.TotalDays);
            log.LogInfo("Tuesday up percent: " + tuesdaydata.CalcUpPercent().ToString("F03")
                + ", Days = " + tuesdaydata.TotalDays);
            log.LogInfo("Wednesday up percent: " + wednesdaydata.CalcUpPercent().ToString("F03")
                + ", Days = " + wednesdaydata.TotalDays);
            log.LogInfo("Thursday up percent: " + thursdaydata.CalcUpPercent().ToString("F03")
                + ", Days = " + thursdaydata.TotalDays);
            log.LogInfo("Friday up percent: " + fridaydata.CalcUpPercent().ToString("F03")
                + ", Days = " + fridaydata.TotalDays);
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
