using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer
{
    class FakeStockHistory : IStockHistory
    {
        public DateTime MaxDate
        {
            get
            {
                return new DateTime(2009, 10, 16);
            }
        }
        public DateTime MinDate
        {
            get
            {
                return new DateTime(2009, 10, 12);
            }
        }

        public int StockId
        {
            get
            {
                return 999999;
            }
        }

        // 得到第一天的股票属性
        public IStockData GetFirstStock()
        {
            return GetStock(MinDate);
        }

        // 得到某一天的股票属性
        public IStockData GetStock(DateTime dt)
        {
            if (_DailyStocks.ContainsKey(dt))
            {
                return _DailyStocks[dt];
            }
            return null;
        }

        public IStockData GetPrevDayStock(DateTime dt)
        {
            DateTime prev = dt.AddDays(-1);
            return GetStock(prev);
        }

        // 得到前一个工作日
        public DateTime GetPrevDay(DateTime dt)
        {
            return dt.AddDays(-1);
        }

        public bool IsOperSuccess(DateTime dt, StockOper oper)
        {
            if (!_DailyStocks.ContainsKey(dt))
            {
                return false;
            }
            if (oper == null)
            {
                return false;
            }

            if (oper.Type == OperType.NoOper)
            {
                return true;
            }

            StockData stock = _DailyStocks[dt];
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

        public void Init()
        {
            DateTime startTime = new DateTime(2009, 10, 12);
            _DailyStocks.Add(startTime,
                FakeStockDataCreator.Create(startTime, 100, 103, 99, 99, 1000));
            _DailyStocks.Add(new DateTime(2009, 10, 13),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 13), 102, 106, 101, 105, 1000));
            _DailyStocks.Add(new DateTime(2009, 10, 14),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 14), 101, 108, 100, 102, 1000));
            _DailyStocks.Add(new DateTime(2009, 10, 15),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 15), 104, 109, 102, 105, 1000));
            _DailyStocks.Add(new DateTime(2009, 10, 16),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 16), 103, 106, 100, 104, 1000));
        }

        private Dictionary<DateTime, StockData> _DailyStocks = new Dictionary<DateTime, StockData>();
    }
}
