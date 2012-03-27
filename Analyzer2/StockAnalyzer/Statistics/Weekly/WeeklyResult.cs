﻿using System;
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
