using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Rise
{
    class VolumeJudger : IStockJudger
    {
        // 依时间次序的连续三天，判断是否满足条件
        // Day1最前，Day2晚一些. 
        public bool FulFil(StockData day1, StockData day2, StockData day3)
        {
            // 成交量放大条件
            return (day3.VolumeHand > day2.VolumeHand) && (day2.VolumeHand > day1.VolumeHand);
        }

        // 依时间次序的连续三天，判断是否满足相反的条件
        // Day1最前，Day2晚一些. 
        public bool ReverseFulFil(StockData day1, StockData day2, StockData day3)
        {
            return (day3.VolumeHand < day2.VolumeHand) && (day2.VolumeHand < day1.VolumeHand);
        }

        public string Name
        {
            get
            {
                return "Volume";
            }
        }
    }
}
