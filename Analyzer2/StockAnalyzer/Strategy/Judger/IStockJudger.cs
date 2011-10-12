using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Judger
{
    public interface IStockJudger
    {
        // 依时间次序的连续三天，判断是否满足条件 
        // Day1最前，Day2晚一些. 
        bool FulFil(IStockData day1, IStockData day2, IStockData day3);

        // 依时间次序的连续三天，判断是否满足相反的条件
        // Day1最前，Day2晚一些. 
        bool ReverseFulFil(IStockData day1, IStockData day2, IStockData day3);

        string Name
        {
            get;
        }
    }
}
