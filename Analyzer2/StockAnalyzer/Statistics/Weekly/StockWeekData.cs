using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Statistics.Weekly
{
    class StockWeekData
    {
        public void AddStockData(IStockData sd)
        {
            if (sd == null)
            {
                return;
            }

            if (sd.EndPrice > sd.StartPrice)
            {
                upCount_++;
            }

            totalCount_++;
        }

        public double CalcUpPercent()
        {
            if (totalCount_ == 0)
            {
                throw new ArithmeticException();
            }

            return (double)upCount_ / totalCount_;
        }

        public int TotalDays
        {
            get
            {
                return totalCount_;
            }
        }

        int upCount_;
        int totalCount_;

    }
}
