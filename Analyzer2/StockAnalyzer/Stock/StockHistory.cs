using System;
using System.Collections.Generic;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Display;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Business.Shape;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    // 保存一只股票一段时间内的历史信息
    public class StockHistory : IStockHistory
    {
        public DateTime MaxDate
        {
            get
            {
                return m_MaxDate;
            }
            set
            {
                m_MaxDate = value;
            }
        }

        public DateTime MinDate
        {
            get
            {
                return m_MinDate;
            }
            set
            {
                m_MinDate = value;
            }
        }

        public int StockId
        {
            get;
            set;
        }

        public void AddStock(DateTime dt, IStockData stock)
        {
            if (!_DailyStocks.ContainsKey(dt))
            {
                _DailyStocks.Add(dt, stock);
            }
            else
            {
                // ???
                // 2009-08-25
            }

            MinDate = (MinDate > dt) ? dt : MinDate;
            MaxDate = (MaxDate < dt) ? dt : MaxDate;
        }

        public void InitAllStocks(int stockId, IList<StockData> arr)
        {
            Clear();

            StockId = stockId;

            foreach (IStockData val in arr)
            {
                AddStock(val.TradeDate, val);
            }
        }

        public void Clear()
        {
            _DailyStocks.Clear();

            m_MaxDate = new DateTime(1900, 1, 1);
            m_MinDate = new DateTime(2100, 1, 1);

            StockId = -1;
        }

        // 得到某一天的股票属性
        public IStockData GetStock(DateTime dt)
        {
            if (!_DailyStocks.ContainsKey(dt))
            {
                return null;
            }
            else
            {
                return _DailyStocks[dt];
            }
        }

        // 得到第一天的股票属性
        public IStockData GetFirstStock()
        {
            DateTime firstDate = MinDate;
            IStockData val = GetStock(firstDate);

            while (val == null)
            {
                firstDate = DateFunc.GetNextWorkday(firstDate);

                val = GetStock(firstDate);
            }

            return val;
        }

        public IStockData GetPrevDayStock(DateTime dt)
        {
            return GetStock(GetPreviousDay(dt));
        }

        // 得到前一个工作日
        public DateTime GetPreviousDay(DateTime dt)
        {
            if (dt <= m_MinDate)
            {
                return m_MinDate;
            }

            DateTime prevDay = DateFunc.GetPreviousWorkday(dt);

            IStockData stock = GetStock(prevDay);
            while (stock == null)
            {
                prevDay = DateFunc.GetPreviousWorkday(prevDay);
                if (prevDay <= m_MinDate)
                {
                    return new DateTime(0);
                }

                stock = GetStock(prevDay);
            }

            return prevDay;
        }

        public bool IsOperSuccess(DateTime dt, StockOper oper)
        {
            if (oper == null)
            {
                return false;
            }

            if (oper.Type == OperType.NoOper)
            {
                return true;
            }

            if (!_DailyStocks.ContainsKey(dt))
            {
                return false;
            }

            IStockData stock = _DailyStocks[dt];
            switch (oper.Type)
            {
                case OperType.Buy:
                    if (oper.UnitPrice >= stock.MinPrice)
                    {
                        return true;
                    }
                    break;
                case OperType.Sell:
                    if (oper.UnitPrice <= stock.MaxPrice)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }

            return false;
        }

        // 绘制股票曲线
        public IStockDrawer GetStockDrawer()
        {
            StockPropDrawer drawer = new StockPropDrawer();

            drawer.MaxDate = m_MaxDate;
            drawer.MinDate = m_MinDate;

            foreach (KeyValuePair<DateTime, IStockData> entry in _DailyStocks)
            {
                drawer.AddDayStock(entry.Key, entry.Value);
            }

            return drawer;
        }

        public void Check(ICustomLog log)
        {
            DateTime startDate = MinDate;
            while (startDate < MaxDate)
            {
                if ((GetStock(startDate) == null) && !Holidays.IsWeekend(startDate))
                {
                    log.LogInfo("Date: " + startDate.ToLongDateString() + ", has no stock data.");
                }
                startDate = startDate.AddDays(1);
            }
        }

        public void JudgeShape(ICustomLog log)
        {
            int tTypeCount = 0;
            int revTTypeCount = 0;
            int crossTypeCount = 0;
            foreach (KeyValuePair<DateTime, IStockData> entry in _DailyStocks)
            {
                if (ShapeJudger.IsCross(entry.Value))
                {
                    crossTypeCount++;
                }

                if (ShapeJudger.IsT(entry.Value))
                {
                    tTypeCount++;
                }

                if (ShapeJudger.IsReverseT(entry.Value))
                {
                    revTTypeCount++;
                }
            }

            log.LogInfo("Total Count = " + _DailyStocks.Count
                + ", Cross = " + crossTypeCount
                + ", T = " + tTypeCount
                + ", Rev T = " + revTTypeCount);
        }

        public static bool IsInRange(double val1, double val2)
        {
            if (val1 > val2 * (1 + COMPRATIO))
            {
                return false;
            }

            if (val1 < val2 * (1 - COMPRATIO))
            {
                return false;
            }

            return true;
        }

        private const double COMPRATIO = 0.1; // 表示每日上涨或者下跌的最大允许值 

        private Dictionary<DateTime, IStockData> _DailyStocks = new Dictionary<DateTime, IStockData>();
        private DateTime m_MaxDate = new DateTime(1900, 1, 1);
        private DateTime m_MinDate = new DateTime(2100, 1, 1);
    }
}
