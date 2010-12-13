using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Rise
{
    public class RiseJudger : IStockJudger
    {
        // 依时间次序的连续三天，判断是否满足条件
        // Day1最前，Day2晚一些. 
        public bool FulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // 连续三天上涨条件
            return (StockJudger.IsRise(day3, day2) && StockJudger.IsRise(day2, day1));
        }

        // 依时间次序的连续三天，判断是否满足相反的条件
        // Day1最前，Day2晚一些. 
        public bool ReverseFulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            // 连续三天下跌条件
            return (!StockJudger.IsRise(day3, day2) && !StockJudger.IsRise(day2, day1));
        }

        public string Name
        {
            get
            {
                return "Compare prev";
            }
        }
    }
}
