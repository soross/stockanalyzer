using System.Collections.Generic;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Judger;
using FinanceAnalyzer.Judger.Validation;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.UI;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// Calculate score of each strategy
    /// strategy comes from StrategyFactory
    /// </summary>
    public class ScoresCalculator
    {
        /// <summary>
        /// Calculate score of each strategy
        /// </summary>
        /// <param name="history">One stock history</param>
        /// <param name="factory">Strategy Factory</param>
        /// <param name="reader">Bonus imformation</param>
        public void Calc(IStockHistory history, IStrategyFactory factory, IBonusProcessor reader)
        {
            FinanceRunner runner = new FinanceRunner();
            runner.CurrentBonusProcessor = reader;
            runner.Go(history, factory);

            IStrategyJudger judger = new StrategyJudger();
            judger.Judge(runner.Results);

            IStrategyJudger judger2 = new ValidationJudger();
            judger2.Judge(runner.Results);

            AllScores_.AddRange(judger.ScoresArr);
            AllScores_.AddRange(judger2.ScoresArr);
        }

        public void ShowResult()
        {
            FormScores scoresForm = new FormScores();

            scoresForm.ScoresArr = AllScores_;
            scoresForm.ShowDialog();
        }

        List<IStrategyScores> AllScores_ = new List<IStrategyScores>();
    }
}
