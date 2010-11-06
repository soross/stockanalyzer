using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Display
{
    public class StockPoint
    {
        // 日期
        public DateTime DateOfStock
        {
            get;
            set;
        }

        public double High
        {
            get;
            set;
        }

        public double Low
        {
            get;
            set;
        }

        public double Open
        {
            get;
            set;
        }

        public double End
        {
            get;
            set;
        }

        // 成交量
        public double Volume
        {
            get;
            set;
        }

        public StockPoint(DateTime dt, double high, double low, double open, double end, double vol)
        {
            DateOfStock = dt;
            High = high;
            Low = low;
            Open = open;
            End = end;
            Volume = vol;
        }
    }
}
