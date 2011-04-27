using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer
{
    class FakeStockDataCreator
    {
        public static StockData Create(DateTime dt, double startPrice, double maxPrice, 
            double minPrice, double endPrice, int volumehand)
        {
            StockData val = new StockData();
            val.Amount = 100000;
            val.Id = 999999;

            val.TradeDate = dt;
            val.StartPrice = startPrice;
            val.EndPrice = endPrice;
            val.MaxPrice = maxPrice;
            val.MinPrice = minPrice;
            val.VolumeHand = volumehand;

            return val;
        }
    }
}
