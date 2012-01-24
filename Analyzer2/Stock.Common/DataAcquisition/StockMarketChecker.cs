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
            int prefix = (stockId / 1000);

            // 500 and 501 mean Shanghai Funds
            if ((prefix == 600) || (prefix == 601)
                || (prefix == 500) || (prefix == 501))
            {
                return true;
            }

            return false;
        }

        public static String ToYahooStockId(int stockId)
        {
            return stockId.ToString() + ".SS";
        }

        public static int YahooStockIdToInt(string yahooId)
        {
            if (string.IsNullOrEmpty(yahooId))
            {
                return -1;
            }

            try
            {
                if (yahooId.Contains(".SS"))
                {
                    string s = yahooId.Replace(".SS", "");
                    return int.Parse(s);
                }
                else
                {
                    return int.Parse(yahooId);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
