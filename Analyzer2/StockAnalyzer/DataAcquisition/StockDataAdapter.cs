using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.DataAcquisition
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
