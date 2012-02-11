using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Judger
{
    /// <summary>
    /// Performance scores of each strategy
    /// </summary>
    public interface IStrategyScores
    {
        /// <summary>
        /// add a score to exist score of one strategy
        /// </summary>
        /// <param name="strategyName">strategy name</param>
        /// <param name="score">score</param>
        void AddScore(string strategyName, double score);

        void SetScore(string strategyName, double score);

        double GetScore(string strategyName);

        string Name
        {
            get;
        }

        ICollection<string> AllStrategyNames
        {
            get;
        }

        // 得到前几个元素
        // 如果集合数目不足，返回全部元素
        ICollection<string> GetTopStrategyNames(int itemCount);
    }
}
