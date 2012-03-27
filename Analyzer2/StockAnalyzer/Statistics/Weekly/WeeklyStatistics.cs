using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Statistics.Weekly
{
    class WeeklyStatistics
    {
        public void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                if (stock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                results_.AddStockData(stock);

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        public void PrintResult()
        {

        }

        WeeklyResults results_ = new WeeklyResults();
    }
}
