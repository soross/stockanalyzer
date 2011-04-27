using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Display;
using Stock.Common.Data;

namespace FinanceAnalyzer.Display
{
    // 绘制股票曲线
    public class StockPropDrawer : IStockDrawer
    {
        public DateTime MinDate
        {
            get
            {
                return _Mindate;
            }
            set
            {
                _Mindate = value;
            }
        }

        public DateTime MaxDate
        {
            get
            {
                return _MaxDate;
            }
            set
            {
                _MaxDate = value;
            }
        }

        public StockPoint GetAt(DateTime dt)
        {
            if (_Stocks.ContainsKey(dt))
            {
                return _Stocks[dt];
            }
            else
            {
                return null;
            }
        }

        // 添加某一天的股票信息
        public void AddDayStock(DateTime dt, IStockData stock)
        {
            StockPoint pt = new StockPoint(dt, stock.MaxPrice, stock.MinPrice,
                stock.StartPrice, stock.EndPrice, stock.VolumeHand);

            _Stocks.Add(dt, pt);
        }

        private Dictionary<DateTime, StockPoint> _Stocks = new Dictionary<DateTime, StockPoint>();
        private DateTime _Mindate;
        private DateTime _MaxDate;
    }
}
