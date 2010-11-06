using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Result;

namespace FinanceAnalyzer.Judger
{
    interface IStrategyJudger
    {
        void Judge(IStrategyResults res);

        ICollection<IStrategyScores> ScoresArr
        {
            get;
        }
    }
}
