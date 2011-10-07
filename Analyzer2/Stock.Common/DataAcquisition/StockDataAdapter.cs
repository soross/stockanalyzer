using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace Stock.Common.DataAcquisition
{
    /// <summary>
    /// Convert from DotNetStock.Engine.Stock to IStockData
    /// </summary>
    class StockDataAdapter
    {
        /// <summary>
        /// Convert from DotNetStock.Engine.Stock to IStockData
        /// </summary>
        /// <param name="stock">DotNetStock.Engine.Stock</param>
        /// <returns>Converted IStockData</returns>
        public static StockData ToStockData(DotNetStock.Engine.Stock stock)
        {
            StockData data = new StockData();

            data.Amount = stock.getVolume();
            data.EndPrice = stock.getLastPrice();
            data.StockId = ConvertStockId(stock.getSymbol().toString());
            data.MaxPrice = stock.getHighPrice();
            data.MinPrice = stock.getLowPrice();
            data.StartPrice = stock.getOpenPrice();
            data.TradeDate = stock.getCalendar().CurrentDate;
            data.VolumeHand = Convert.ToInt32(stock.getVolume());
            return data;
        }

        static int ConvertStockId(string symbol)
        {
            return StockMarketChecker.YahooStockIdToInt(symbol);
        }
    }
}
