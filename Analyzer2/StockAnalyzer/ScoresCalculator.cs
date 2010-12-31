using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Judger;
using FinanceAnalyzer.Judger.Validation;
using FinanceAnalyzer.UI;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business;

namespace FinanceAnalyzer
{
    class ScoresCalculator
    {
        public static void Calc(IStockHistory history, IStrategyFactory factory, IBonusProcessor reader)
        {
            FinanceRunner runner = new FinanceRunner();
            runner.CurrentBounsProcessor = reader;
            runner.Go(history, factory);

            IStrategyJudger judger = new StrategyJudger();
            judger.Judge(runner.Results);

            IStrategyJudger judger2 = new ValidationJudger();
            judger2.Judge(runner.Results);

            FormScores scoresForm = new FormScores();

            ICollection<IStrategyScores> arr = judger.ScoresArr;

            foreach (IStrategyScores scores in judger2.ScoresArr)
            {
                arr.Add(scores);
            }

            scoresForm.ScoresArr = arr;
            scoresForm.ShowDialog();
        }
    }
}
