using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Judger
{
    /// <summary>
    /// Compare stock total value of each days to the hold strategy, and accumulate it to the total score. 
    /// </summary>
    class StrategyJudger : IStrategyJudger
    {
        public void Judge(IStrategyResults res)
        {
            ICollection<string> allStrategies = res.AllStrategyNames;

            IStockValues holderValues = res.GetResult("Hold");
            List<DateTime> dates = holderValues.GetAllDate().ToList<DateTime>();

            dates.Sort();
            DateTime minDate = dates.First<DateTime>();
            DateTime maxDate = dates.Last<DateTime>();
            Debug.Assert(minDate < maxDate);

            DateTime curDate = minDate;
            while (curDate < maxDate)
            {
                foreach (string name in allStrategies)
                {
                    IStockValues values = res.GetResult(name);

                    double holderTotalValue = holderValues.GetTotalValue(curDate);

                    double strategyTotalValue = values.GetTotalValue(curDate);

                    Scores_.AddScore(name, CalcSimpleScore(holderTotalValue, strategyTotalValue));
                }

                curDate = DateFunc.GetNextWorkday(curDate);
            }
        }

        public ICollection<IStrategyScores> ScoresArr
        {
            get
            {
                ICollection<IStrategyScores> arr = new List<IStrategyScores>();
                arr.Add(Scores_);
                return arr;
            }
        }

        // 按照每天的策略市值与持有市值的相对百分比计算
        private static double CalcSimpleScore(double holderToday, double valToday)
        {
            double delta = (valToday - holderToday) / holderToday;
            return delta;
        }

        IStrategyScores Scores_ = new StrategyScores("Daily Prices Sigma");
    }
}
