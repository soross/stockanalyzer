using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace Stock.Common.DataAcquisition
{
    class StockDataAdapter
    {
        public static IStockData ToStockData(DotNetStock.Engine.Stock stock)
        {
            StockData data = new StockData();

            data.Amount = stock.getVolume();

            return data;
        }
    }
}
