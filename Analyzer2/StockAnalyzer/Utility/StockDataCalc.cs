using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Utility
{
    class StockDataCalc
    {
        public static double GetRisePercent(IStockData stock)
        {
            return (stock.EndPrice - stock.StartPrice) / stock.StartPrice;
        }

        public static bool PriceInRange(IStockData stock, double price)
        {
            return (price >= stock.MinPrice) && (price <= stock.MaxPrice);
        }
    }
}
