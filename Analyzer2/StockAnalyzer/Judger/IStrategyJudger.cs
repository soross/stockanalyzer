using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Result;

namespace FinanceAnalyzer.Judger
{
    /// <summary>
    /// Judge each strategy performance
    /// </summary>
    interface IStrategyJudger
    {
        /// <summary>
        /// Judge each strategy performance
        /// </summary>
        /// <param name="res">strategy trade results</param>
        void Judge(IStrategyResults res);

        /// <summary>
        /// Return strategy scores
        /// </summary>
        ICollection<IStrategyScores> ScoresArr
        {
            get;
        }
    }
}
