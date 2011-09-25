using System;
using System.Collections.Generic;
using System.Text;

[assembly: CLSCompliant(true)]

namespace FinanceAnalyzer.Stock
{
    public class MarketStock
    {
        public MarketStock()
        {
        }

        public double TotalPrice
        {
            get
            {
                return _UnitPrice * StockCount;
            }            
        }

        public void Buy(int count, double unitPrice)
        {
            if (unitPrice < 0)
            {
                throw new ArgumentOutOfRangeException("unitPrice", "Unit price less than zero");
            }

            CalcNewPrice(count, unitPrice);
        }

        public double Sell(int count, double unitPrice)
        {
            if (count > StockCount)
            {
                throw new ArgumentOutOfRangeException("count", "Stock Count out of range");
            }

            if (unitPrice <= 0)
            {
                throw new ArgumentOutOfRangeException("unitPrice", "Unit price less than zero");
            }

            CalcNewPrice(-count, unitPrice);

            return (count * unitPrice);
        }

        private void CalcNewPrice(int count, double unitPrice)
        {
            double prevCount = StockCount;
            StockCount += count;
            if (StockCount > 0)
            {
                _UnitPrice = ((prevCount * _UnitPrice) + (count * unitPrice)) / StockCount;
            }
            else
            {
                _UnitPrice = 0;
            }
        }

        public int Count
        {
            get
            {
                return StockCount;
            }
            set
            {
                StockCount = value;
            }
        }

        public double UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }

        private int StockCount;
        private double _UnitPrice;

    }
}
