using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Result;
using System.Diagnostics;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Judger.Validation
{
    // Calculate the validation of each buy and sell signal
    // If stock trend was down during a sell and a buy signal, or up during a buy and sell signal, 
    // then added this percent as a positive score. 
    class ValidationJudger : IStrategyJudger
    {
        public void Judge(IStrategyResults res)
        {
            _HoldValues = res.GetResult("Hold");

            FindMaxMinDate();

            foreach (string strategyName in res.AllStrategyNames)
            {
                IStockValues values = res.GetResult(strategyName);

                JudgeStrategy(strategyName, values);
            }
        }

        public ICollection<IStrategyScores> ScoresArr
        {
            get
            {
                ICollection<IStrategyScores> _ScoresArr = new List<IStrategyScores>();

                _ScoresArr.Add(_BuyScores);
                _ScoresArr.Add(_SellScores);
                _ScoresArr.Add(_Scores);
                return _ScoresArr;
            }
        }

        private void FindMaxMinDate()
        {
            List<DateTime> dates = _HoldValues.GetAllDate().ToList<DateTime>();

            dates.Sort();
            _MinDate = dates.First<DateTime>();
            _MaxDate = dates.Last<DateTime>();
            Debug.Assert(_MinDate < _MaxDate);
        }

        private void JudgeStrategy(string strategyName, IStockValues values)
        {
            SignalValidationCalc calc = new SignalValidationCalc();
            calc.HoldValues = _HoldValues;

            DateTime curDate = _MinDate;
            while (curDate < _MaxDate)
            {
                OperType tp = values.GetOperationSignal(curDate);

                calc.AddSignal(curDate, tp);

                curDate = DateFunc.GetNextWorkday(curDate);
            }

            _Scores.SetScore(strategyName, calc.TotalScore);
            _BuyScores.SetScore(strategyName, calc.BuyScore);
            _SellScores.SetScore(strategyName, calc.SellScore);
        }

        DateTime _MinDate;
        DateTime _MaxDate;

        IStockValues _HoldValues;

        IStrategyScores _Scores = new StrategyScores("Buy and Sell Signal");

        IStrategyScores _BuyScores = new StrategyScores("Buy Signal");
        IStrategyScores _SellScores = new StrategyScores("Sell Signal");
    }
}
