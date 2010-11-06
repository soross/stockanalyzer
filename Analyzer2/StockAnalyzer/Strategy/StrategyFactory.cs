using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Strategy.Rise;
using FinanceAnalyzer.Strategy.Indicator;
using FinanceAnalyzer.Strategy.Impl;

namespace FinanceAnalyzer.Strategy
{
    public class StrategyFactory : IStrategyFactory
    {
        public virtual void Init()
        {
            AddStrategy(new StrategyMinMax());
            AddStrategy(new StrategyKD(25, 75));
            AddStrategy(new StrategyBamboo());

            AddStrategy(new StrategyVolumeOptim(0.4, 0.3));

            AddStrategy(new StrategyPeriodicity());

            AddStrategy(new StrategyThreeDay(new RiseJudger()));
            AddStrategy(new StrategyThreeDay(new UpJudger()));
            AddStrategy(new StrategyThreeDay(new VolumeJudger()));
            AddStrategy(new StrategyThreedayOpti(new TwoDaysUpJudger()));
            AddStrategy(new StrategyThreeDay(new RiseJudgerOptim()));
            AddStrategy(new StrategyThreeDay(new UpJudgerOptim()));

            IMixedCalc mixedCalc = new IMixedCalc();
            mixedCalc.SetIndicator(new ThreeDayCalc(new UpJudger()), IndicatorMixedType.BuyAndSell);
            mixedCalc.SetIndicator(new MoneyFlowIndexCalc(), IndicatorMixedType.BuyAndSell);
            AddStrategyByIndicator(mixedCalc);

            IIndicatorCalc calc = new ThreeDayCalc(new UpJudger());
            AddStrategyByIndicator(calc);

            AddStrategyByIndicator(new MacdCalculator());
            AddStrategyByIndicator(new RsiCalculator());
            AddStrategyByIndicator(new MoneyFlowIndexCalc());
            AddStrategyByIndicator(new RiseDownCalc(0.06));
            AddStrategyByIndicator(new HoldCalc());
            AddStrategyByIndicator(new KdCalc(25, 75));

            AddStrategyByIndicator(new SimpleShapeCalc());

            const double BUYMARGINPERCENT = 0.3; // 门限
            const double SELLMARGINPERCENT = 0.6;
            AddStrategyByIndicator(new VolumeCalc(BUYMARGINPERCENT, SELLMARGINPERCENT));

            StrategyPercent strategy = new StrategyPercent(0.1);
            AddStrategy(strategy);

            StrategyBear strategy2 = new StrategyBear();
            strategy2.Profit = 0.05;
            AddStrategy(strategy2);
        }

        public void AddStrategy(IFinanceStrategy strategy)
        {
            _AllStrategies.Add(strategy.Name, strategy);
        }

        public void AddStrategyByIndicator(IIndicatorCalc calc)
        {
            AddStrategy(new StrategyIndicator(calc));
        }

        public IFinanceStrategy GetStrategy(string strategyName)
        {
            if (_AllStrategies.ContainsKey(strategyName))
            {
                return _AllStrategies[strategyName];
            }
            else
            {
                return null;
            }
        }

        public ICollection<string> AllStrategyNames
        {
            get
            {
                return _AllStrategies.Keys;
            }
        }

        public ICollection<IFinanceStrategy> AllStrategies
        {
            get
            {
                return _AllStrategies.Values;
            }
        }

        public void Remove(string strategyName)
        {
            if (_AllStrategies.ContainsKey(strategyName))
            {
                _AllStrategies.Remove(strategyName);
            }
            else
            {
            }
        }

        private Dictionary<string, IFinanceStrategy> _AllStrategies = new Dictionary<string, IFinanceStrategy>();
    }
}
