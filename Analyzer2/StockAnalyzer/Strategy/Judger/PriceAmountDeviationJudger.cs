using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Rise
{
    class PriceAmountDeviationJudger : IStockJudger
    {
        public bool FulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            if (StockJudger.IsUp(day1) && StockJudger.IsUp(day2) && StockJudger.IsUp(day3))
            {
                if ((day1.Amount < day2.Amount) && (day1.Amount < day3.Amount))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ReverseFulFil(IStockData day1, IStockData day2, IStockData day3)
        {
            return false;
        }

        public string Name
        {
            get
            {
                return "Deviation";
            }
        }
    }
}
