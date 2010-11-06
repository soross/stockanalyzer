using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Rise
{    
    public class RiseJudgerOptim : IStockJudger
    {
        // 依时间次序的连续三天，判断是否满足条件
        // Day1最前，Day2晚一些. 
        public bool FulFil(StockData day1, StockData day2, StockData day3)
        {
            // 连续三天上涨条件
            return StockJudger.IsUp(day1) && StockJudger.IsUp(day2) && StockJudger.IsUp(day3) && IsWin(day3);
        }

        // 依时间次序的连续三天，判断是否满足相反的条件
        // Day1最前，Day2晚一些. 
        public bool ReverseFulFil(StockData day1, StockData day2, StockData day3)
        {
            // 连续三天下跌条件
            return !StockJudger.IsUp(day1) && !StockJudger.IsUp(day2) && !StockJudger.IsUp(day3);
        }

        public string Name
        {
            get
            {
                return "Compare prev optim";
            }
        }

        private bool IsWin(StockData daystock)
        {
            if (HolderManager.Instance().Holder.HasStock())
            {
                return (daystock.EndPrice >= HolderManager.Instance().Holder.UnitPrice);
            }
            else
            {
                return true;
            }
        }
    }
}
