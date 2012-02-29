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
        private StockDataCalc()
        {
        }

        public static double GetRisePercent(IStockData stock)
        {
            return GetRisePercent(stock.StartPrice, stock.EndPrice);
        }

        public static double GetRisePercent(double basePrice, double anotherPrice)
        {
            return (anotherPrice - basePrice) / basePrice;
        }

        public static bool PriceInRange(IStockData stock, double price)
        {
            return (price >= stock.MinPrice) && (price <= stock.MaxPrice);
        }
    }
}
