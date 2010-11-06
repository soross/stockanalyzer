using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Result;
using System.Diagnostics;

namespace FinanceAnalyzer.Judger
{
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

                    _Scores.AddScore(name, CalcSimpleScore(holderTotalValue, strategyTotalValue));
                }

                curDate = DateFunc.GetNextWorkday(curDate);
            }
        }

        public ICollection<IStrategyScores> ScoresArr
        {
            get
            {
                ICollection<IStrategyScores> arr = new List<IStrategyScores>();
                arr.Add(_Scores);
                return arr;
            }
        }

        // 按照每天的策略市值与持有市值的相对百分比计算
        private static double CalcSimpleScore(double holderToday, double valToday)
        {
            double delta = (valToday - holderToday) / holderToday;
            return delta * 100;
        }

        // ??
        private static double CalcScore(double holderToday, double holderTomorrow, double valToday, double valTomorrow)
        {
            double holderPercent = calcPercent(holderToday, holderTomorrow);
            double valPercent = calcPercent(valToday, valTomorrow);
            return (valPercent - holderPercent) * 100;
        }

        private static double calcPercent(double holderToday, double holderTomorrow)
        {
            double holderPercent = (holderTomorrow - holderToday) / holderToday;
            return holderPercent;
        }

        IStrategyScores _Scores = new StrategyScores("Daily Prices Sigma");
    }
}
