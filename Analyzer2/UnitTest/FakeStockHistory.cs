using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer
{
    class FakeStockHistory : IStockHistory
    {
        public DateTime MaxDate
        {
            get
            {
                return AllDates_[AllDates_.Count - 1];
            }
        }
        public DateTime MinDate
        {
            get
            {
                return AllDates_[0];
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
            if (DailyStocks_.ContainsKey(dt))
            {
                return DailyStocks_[dt];
            }
            return null;
        }

        public IStockData GetPrevDayStock(DateTime dt)
        {
            DateTime prev = dt.AddDays(-1);
            return GetStock(prev);
        }

        // 得到前一个工作日
        public DateTime GetPreviousDay(DateTime dt)
        {
            return dt.AddDays(-1);
        }

        public bool IsOperSuccess(DateTime dt, StockOper oper)
        {
            if (!DailyStocks_.ContainsKey(dt))
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

            StockData stock = DailyStocks_[dt];
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
            InitStocks();
        }

        public void AddStock(DateTime dt, IStockData stock)
        {
        }

        public IStockHistory GetPartStockHistory(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        private void InitStocks()
        {
            int currentDate = 1;
            AddDateStock(currentDate, 100, 100, 105, 95, 1000); currentDate++;
            AddDateStock(currentDate, 102, 103, 105, 95, 1200); currentDate++;
            AddDateStock(currentDate, 105, 108, 108, 99, 1400); currentDate++;
            AddDateStock(currentDate, 100, 100, 105, 95, 800); currentDate++;
            AddDateStock(currentDate, 100, 90, 100, 90, 1000); currentDate++;
            AddDateStock(currentDate, 95, 103, 105, 95, 1000); currentDate++;
            AddDateStock(currentDate, 100, 97, 101, 96, 1000); currentDate++;
            AddDateStock(currentDate, 97, 93, 98, 90, 900); currentDate++;
            AddDateStock(currentDate, 93, 89, 94, 89, 800); currentDate++;
            AddDateStock(currentDate, 100, 101, 102, 99, 3000); currentDate++;
            AddDateStock(currentDate, 97, 100, 105, 97, 1000); currentDate++;
            AddDateStock(currentDate, 96, 97, 98, 95, 1000); currentDate++;
            AddDateStock(currentDate, 100, 102, 102, 91, 1000); currentDate++;
            AddDateStock(currentDate, 91, 89, 93, 87, 4000); currentDate++;
            AddDateStock(currentDate, 93, 95, 97, 91, 1000); currentDate++;
            AddDateStock(currentDate, 95, 97, 97, 95, 2000); currentDate++;
            AddDateStock(currentDate, 99, 100, 105, 95, 1000); currentDate++;
            AddDateStock(currentDate, 100, 101, 105, 95, 1000); currentDate++;
            AddDateStock(currentDate, 101, 100, 105, 95, 1000); currentDate++;
            AddDateStock(currentDate, 96, 100, 105, 95, 1000); currentDate++;
            AddDateStock(currentDate, 100, 98, 105, 95, 3000); currentDate++;
            AddDateStock(currentDate, 94, 98, 100, 95, 500); currentDate++;
            AddDateStock(currentDate, 100, 100, 105, 95, 1000); currentDate++;

            AllDates_.Clear();
            AllDates_.AddRange(DailyStocks_.Keys);
            AllDates_.Sort();
        }

        private void InitSTocks2()
        {
            DateTime startTime = new DateTime(2009, 10, 12);
            DailyStocks_.Add(startTime,
                FakeStockDataCreator.Create(startTime, 100, 103, 99, 99, 1000));
            DailyStocks_.Add(new DateTime(2009, 10, 13),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 13), 102, 106, 101, 105, 1500));
            DailyStocks_.Add(new DateTime(2009, 10, 14),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 14), 101, 108, 100, 102, 800));
            DailyStocks_.Add(new DateTime(2009, 10, 15),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 15), 104, 109, 102, 105, 1100));
            DailyStocks_.Add(new DateTime(2009, 10, 16),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 16), 103, 106, 100, 104, 900));

            DailyStocks_.Add(new DateTime(2009, 10, 17),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 17), 102, 106, 101, 105, 2000));
            DailyStocks_.Add(new DateTime(2009, 10, 18),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 18), 101, 108, 100, 102, 1000));
            DailyStocks_.Add(new DateTime(2009, 10, 19),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 19), 104, 109, 102, 105, 1000));
            DailyStocks_.Add(new DateTime(2009, 10, 20),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 20), 103, 106, 100, 104, 1000));

            DailyStocks_.Add(new DateTime(2009, 10, 21),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 21), 102, 106, 101, 105, 1300));
            DailyStocks_.Add(new DateTime(2009, 10, 22),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 22), 101, 108, 100, 102, 1000));
            DailyStocks_.Add(new DateTime(2009, 10, 23),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 23), 104, 109, 102, 105, 1600));
            DailyStocks_.Add(new DateTime(2009, 10, 24),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 24), 103, 106, 100, 104, 1000));

            DailyStocks_.Add(new DateTime(2009, 10, 25),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 25), 102, 106, 101, 105, 600));
            DailyStocks_.Add(new DateTime(2009, 10, 26),
                FakeStockDataCreator.Create(new DateTime(2009, 10, 26), 101, 108, 100, 102, 1000));

            AllDates_.Clear();
            AllDates_.AddRange(DailyStocks_.Keys);
            AllDates_.Sort();
        }

        private void AddDateStock(int date, double startPrice, double endPrice, double maxPrice, double minPrice, int volumehand)
        {
            DateTime dt = new DateTime(2009, 10, date);
            DailyStocks_.Add(dt, FakeStockDataCreator.Create(dt, startPrice, maxPrice, minPrice, endPrice, volumehand));
        }
        private Dictionary<DateTime, StockData> DailyStocks_ = new Dictionary<DateTime, StockData>();
        private List<DateTime> AllDates_ = new List<DateTime>();

    }
}
