using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Strategy.Judger;
using FinanceAnalyzer.Strategy.Indicator;
using FinanceAnalyzer.Strategy.Impl;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Business.Shape;
using FinanceAnalyzer.Strategy.Indicator.Signal;

namespace FinanceAnalyzer.Strategy
{
    public class StrategyFactory : IStrategyFactory
    {
        public virtual void Init()
        {
            AddStrategyBySignal(new ThreeDaySignal(new UpJudger()));
            AddStrategyBySignal(new RiseDownSignal(0.06));
            AddStrategyBySignal(new RSISignal());
            AddStrategyBySignal(new HoldSignal());
            AddStrategyBySignal(new KDSignal(25, 75));
            AddStrategyBySignal(new MACDSignal());
            AddStrategyBySignal(new MoneyFlowIndexSignal());
            AddStrategyBySignal(new MovingAverageSignal());
            AddStrategyBySignal(new EngulfingSignal());
            AddStrategyBySignal(new SpikeShapeSignal(0.03));
            AddStrategyBySignal(new SpikeVolumeShapeSignal(0.025));
            AddStrategyBySignal(new SimpleShapeSignal());
            AddStrategyBySignal(new TripleShapeSignal(new TripleShapeScanner()));

            AddStrategyBySignal(new WaysShapeSignal(new ShapeScanner()));

            const double BUYMARGINPERCENT = 0.3; // 门限
            const double SELLMARGINPERCENT = 0.6;
            AddStrategyBySignal(new VolumeSignal(BUYMARGINPERCENT, SELLMARGINPERCENT));

            AddMixedIndicators(new BasicSignalCalc(new ThreeDaySignal(new UpJudger())),
                new BasicSignalCalc(new MoneyFlowIndexSignal()));
            AddMixedIndicators(new BasicSignalCalc(new SimpleShapeSignal()), new BasicSignalCalc(new MACDSignal()));

            AddStrategy(new StrategyKD(25, 75));
            AddStrategy(new StrategyTwoDayPlusOne());
            AddStrategy(new StrategyMinMax());
            AddStrategy(new StrategyBamboo());
            AddStrategy(new StrategyVolumeOptim(0.4, 0.3));

            StrategyPercent strategy = new StrategyPercent(0.1);
            AddStrategy(strategy);

            StrategyBear strategy2 = new StrategyBear();
            strategy2.Profit = 0.05;
            AddStrategy(strategy2);
        }

        public void AddStrategy(IFinanceStrategy strategy)
        {
            AllStrategies_.Add(strategy.Name, strategy);
        }

        protected void AddStrategyByIndicator(IIndicatorCalc calc)
        {
            AddStrategy(new StrategyIndicator(calc));
        }

        protected void AddStrategyBySignal(ISignalCalculator calc)
        {
            AddStrategyByIndicator(new BasicSignalCalc(calc));
        }

        private void AddMixedIndicators(IIndicatorCalc calc1, IIndicatorCalc calc2)
        {
            MixedCalc mixedCalc = new MixedCalc();
            mixedCalc.AddIndicator(calc1, IndicatorMixedType.BuyAndSell);
            mixedCalc.AddIndicator(calc2, IndicatorMixedType.BuyAndSell);
            AddStrategyByIndicator(mixedCalc);
        }

        private void AddMixedIndicators(IIndicatorCalc calc1, IndicatorMixedType type1,
            IIndicatorCalc calc2, IndicatorMixedType type2)
        {
            MixedCalc mixedCalc = new MixedCalc();
            mixedCalc.AddIndicator(calc1, type1);
            mixedCalc.AddIndicator(calc2, type2);
            AddStrategyByIndicator(mixedCalc);
        }

        public IFinanceStrategy GetStrategy(string strategyName)
        {
            if (AllStrategies_.ContainsKey(strategyName))
            {
                return AllStrategies_[strategyName];
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
                return AllStrategies_.Keys;
            }
        }

        public ICollection<IFinanceStrategy> AllStrategies
        {
            get
            {
                return AllStrategies_.Values;
            }
        }

        public void Remove(string strategyName)
        {
            if (AllStrategies_.ContainsKey(strategyName))
            {
                AllStrategies_.Remove(strategyName);
            }
            else
            {
            }
        }

        private Dictionary<string, IFinanceStrategy> AllStrategies_ = new Dictionary<string, IFinanceStrategy>();
    }
}
