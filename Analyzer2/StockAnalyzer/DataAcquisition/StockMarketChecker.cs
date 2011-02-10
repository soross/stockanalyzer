using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.DataAcquisition
{
    class StockMarketChecker
    {
        public static bool IsChinaShanghaiStock(int stockId)
        {
            return (stockId / 100) == 600;
        }

        public static String ToYahooStockId(int stockId)
        {
            return stockId.ToString() + ".SS";
        }
    }
}
