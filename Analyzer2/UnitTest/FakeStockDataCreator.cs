using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer
{
    class FakeStockDataCreator
    {
        /// <summary>
        /// 创建假的股票数据供测试
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="minPrice"></param>
        /// <param name="endPrice"></param>
        /// <param name="volumehand"></param>
        /// <returns></returns>
        public static StockData Create(DateTime dt, double startPrice, double maxPrice, 
            double minPrice, double endPrice, int volumehand)
        {
            StockData val = new StockData();
            val.Amount = 100000;
            val.StockId = 999999;

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
