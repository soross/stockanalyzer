﻿using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Rise
{
    public interface IStockJudger
    {
        // 依时间次序的连续三天，判断是否满足条件 
        // Day1最前，Day2晚一些. 
        bool FulFil(StockData day1, StockData day2, StockData day3);

        // 依时间次序的连续三天，判断是否满足相反的条件
        // Day1最前，Day2晚一些. 
        bool ReverseFulFil(StockData day1, StockData day2, StockData day3);

        string Name
        {
            get;
        }
    }
}
