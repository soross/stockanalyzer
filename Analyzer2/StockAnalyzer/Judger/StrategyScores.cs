using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Judger
{
    class StrategyScores : IStrategyScores
    {
        public StrategyScores(string val)
        {
            Name = val;
        }

        public void AddScore(string strategyName, double score)
        {
            if (_StrategyToScore.ContainsKey(strategyName))
            {
                double curScore = _StrategyToScore[strategyName];
                curScore += score;

                _StrategyToScore[strategyName] = curScore;
            }
            else
            {
                _StrategyToScore.Add(strategyName, score);
            }
        }

        public void SetScore(string strategyName, double score)
        {
            if (!_StrategyToScore.ContainsKey(strategyName))
            {
                _StrategyToScore.Add(strategyName, score);
            }
            else
            {
                _StrategyToScore[strategyName] = score;
            }
        }

        public double GetScore(string strategyName)
        {
            if (_StrategyToScore.ContainsKey(strategyName))
            {
                return _StrategyToScore[strategyName];
            }
            else
            {
                return -1;
            }
        }

        public ICollection<string> AllStrategyNames
        {
            get
            {
                return _StrategyToScore.Keys;
            }
        }

        public string Name
        {
            set;
            get;
        }

        public ICollection<string> GetTopStrategyNames(int itemCount)
        {
            //sort with value
            var result = from item in _StrategyToScore
                         orderby item.Value descending
                         select item;

            ICollection<string> names = new List<string>();
            foreach (var item in result)
            {
                names.Add(item.Key);

                if (names.Count == itemCount)
                {
                    break;
                }
            }

            return names;
        }

        private Dictionary<string, double> _StrategyToScore = new Dictionary<string, double>();
    }
}
