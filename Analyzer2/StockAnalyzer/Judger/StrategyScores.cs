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
            if (StrategyNameToScore_.ContainsKey(strategyName))
            {
                double curScore = StrategyNameToScore_[strategyName];
                curScore += score;

                StrategyNameToScore_[strategyName] = curScore;
            }
            else
            {
                StrategyNameToScore_.Add(strategyName, score);
            }
        }

        public void SetScore(string strategyName, double score)
        {
            if (!StrategyNameToScore_.ContainsKey(strategyName))
            {
                StrategyNameToScore_.Add(strategyName, score);
            }
            else
            {
                StrategyNameToScore_[strategyName] = score;
            }
        }

        public double GetScore(string strategyName)
        {
            if (StrategyNameToScore_.ContainsKey(strategyName))
            {
                return StrategyNameToScore_[strategyName];
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
                return StrategyNameToScore_.Keys;
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
            var result = from item in StrategyNameToScore_
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

        private Dictionary<string, double> StrategyNameToScore_ = new Dictionary<string, double>();
    }
}
