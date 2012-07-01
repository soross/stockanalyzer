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

            AddMixedSignals(new ThreeDaySignal(new UpJudger()),
                new MoneyFlowIndexSignal());
            AddMixedSignals(new SimpleShapeSignal(), new MACDSignal());

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

        protected void AddStrategyBySignal(ISignalCalculator calc)
        {
            AddStrategy(new StrategyIndicator(new BasicSignalCalc(calc)));
        }

        private void AddMixedSignals(ISignalCalculator calc1, ISignalCalculator calc2)
        {
            MixMultiSignals signals = new MixMultiSignals();
            signals.AddIndicator(calc1, IndicatorMixedType.BuyAndSell);
            signals.AddIndicator(calc2, IndicatorMixedType.BuyAndSell);

            AddStrategyBySignal(signals);
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
