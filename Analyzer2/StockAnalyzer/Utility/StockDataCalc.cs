using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Utility
{
    class StockDataCalc
    {
        public static double GetRisePercent(IStockData stock)
        {
            return (stock.EndPrice - stock.StartPrice) / stock.StartPrice;
        }
    }
}
