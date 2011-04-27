using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Rise
{
    class TwoDaysUpVolJudger : IStockJudger
    {
        // 依时间次序的连续两天，判断是否满足条件
        // Day1最前，Day2晚一些. 
        public bool FulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // 连续两天上涨，并且交易量下降条件
            return StockJudger.IsUp(day1) && StockJudger.IsUp(day2) && (day2.VolumeHand < day1.VolumeHand);
        }

        // 依时间次序的连续两天，判断是否满足相反的条件
        // Day1最前，Day2晚一些. 
        public bool ReverseFulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // 连续两天下跌，并且交易量下降条件
            return !StockJudger.IsUp(day1) && !StockJudger.IsUp(day2) && (day2.VolumeHand < day1.VolumeHand);
        }

        public string Name
        {
            get
            {
                return "2days up vol";
            }
        }
    }
}
