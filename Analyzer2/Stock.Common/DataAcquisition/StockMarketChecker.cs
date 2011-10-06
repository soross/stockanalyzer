using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Common.DataAcquisition
{
    class StockMarketChecker
    {
        /// <summary>
        /// Check if Shanghai board stock 
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public static bool IsChinaShanghaiStock(int stockId)
        {
            // TODO: Questionable with funds
            return (stockId / 1000) == 600;
        }

        public static String ToYahooStockId(int stockId)
        {
            return stockId.ToString() + ".SS";
        }
    }
}
