using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.DB
{
    public sealed class StockJudger
    {
        // 今天的收盘相对于开盘是否上涨
        public static bool IsUp(StockData data)
        {
            return data.EndPrice > data.StartPrice;
        }

        // 今天的收盘价相对于昨天是否上涨
        public static bool IsRise(StockData curStock, StockData prev)
        {
            return curStock.EndPrice > prev.EndPrice;
        }

        private StockJudger()
        {
        }
    }
}
