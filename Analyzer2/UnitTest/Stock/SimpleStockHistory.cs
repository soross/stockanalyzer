using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// StockHistory 测试类
    /// </summary>
    class SimpleStockHistory : IStockHistory
    {
        #region IStockHistory Members

        public DateTime MaxDate
        {
            get { return new DateTime(2012,2,24); }
        }

        public DateTime MinDate
        {
            get { return new DateTime(2012, 2, 21); }
        }

        public int StockId
        {
            get { return 900000; }
        }

        public global::Stock.Common.Data.IStockData GetFirstStock()
        {
            return stocks_[startDate_];
        }

        public global::Stock.Common.Data.IStockData GetStock(DateTime dt)
        {
            return stocks_[dt];
        }

        public global::Stock.Common.Data.IStockData GetPrevDayStock(DateTime dt)
        {
            dt = dt.AddDays(-1);

            if (stocks_.ContainsKey(dt))
            {
                return stocks_[dt];
            }
            else
            {
                return null;
            }
        }

        public DateTime GetPreviousDay(DateTime dt)
        {
            return dt.AddDays(-1);
        }

        public bool IsOperSuccess(DateTime dt, StockOper oper)
        {
            throw new NotImplementedException();
        }

        public void AddStock(DateTime dt, global::Stock.Common.Data.IStockData stock)
        {
            throw new NotImplementedException();
        }

        public IStockHistory GetPartStockHistory(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        #endregion

        public SimpleStockHistory()
        {
            DateTime dt = startDate_;
            stocks_.Add(dt, FakeStockDataCreator.Create(dt, 10,11,9,10,30));
            dt = dt.AddDays(1);

            stocks_.Add(dt, FakeStockDataCreator.Create(dt, 10, 10.2, 9.9, 10.2, 50));
            dt = dt.AddDays(1);

            stocks_.Add(dt, FakeStockDataCreator.Create(dt, 10, 10.3, 9.8, 9.8, 30));
            dt = dt.AddDays(1);

            stocks_.Add(dt, FakeStockDataCreator.Create(dt, 10, 10.5, 9.6, 9.8, 30));
            dt = dt.AddDays(1);
        }

        DateTime startDate_ = new DateTime(2012,2,21);
        Dictionary<DateTime, IStockData> stocks_ = new Dictionary<DateTime, IStockData>();
    }
}
