using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Rise
{
    class TwoDaysUpJudger : IStockJudger
    {
        // 依时间次序的连续两天，判断是否满足条件
        // Day1最前，Day2晚一些. 
        public bool FulFil(StockData day1, StockData day2, StockData day3)
        {
            // 连续两天上涨条件
            return StockJudger.IsUp(day1) && StockJudger.IsUp(day2);
        }

        // 依时间次序的连续两天，判断是否满足相反的条件
        // Day1最前，Day2晚一些. 
        public bool ReverseFulFil(StockData day1, StockData day2, StockData day3)
        {
            // 连续两天下跌条件
            return !StockJudger.IsUp(day1) && !StockJudger.IsUp(day2);
        }

        public string Name
        {
            get
            {
                return "2days up";
            }
        }
    }
}
