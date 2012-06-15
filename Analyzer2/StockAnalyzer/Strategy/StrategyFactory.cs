using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Strategy.Judger;
using FinanceAnalyzer.Strategy.Indicator;
using FinanceAnalyzer.Strategy.Impl;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Business.Shape;
using FinanceAnalyzer.Strategy.Indicator.Shape;
using FinanceAnalyzer.Strategy.Indicator.Signal;

namespace FinanceAnalyzer.Strategy
{
    public class StrategyFactory : IStrategyFactory
    {
        public virtual void Init()
        {            
            AddStrategy(new StrategyKD(25, 75));
            AddStrategy(new StrategyTwoDayPlusOne());
            AddStrategy(new StrategyThreeDay(new RiseJudger()));
            AddStrategy(new StrategyThreeDay(new UpJudger()));
            AddMixedIndicators(new ThreeDayCalc(new UpJudger()), new MoneyFlowIndexCalc());

            IIndicatorCalc calc = new ThreeDayCalc(new UpJudger());
            AddStrategyByIndicator(calc);

            AddStrategyByIndicator(new RsiCalculator());
            AddStrategyByIndicator(new MoneyFlowIndexCalc());
            AddStrategyByIndicator(new RiseDownCalc(0.06));
            AddStrategyByIndicator(new HoldCalc());
            AddStrategyByIndicator(new KdCalc(25, 75));
            
            AddStrategyByIndicator(new EngulfingCalc());

            AddStrategyByIndicator(new BasicSignalCalc(new MACDSignal()));
            AddStrategyByIndicator(new BasicSignalCalc(new MoneyFlowIndexSignal()));

            AddStrategyByIndicator(new SimpleShapeCalc());
            AddStrategyByIndicator(new SpikeShapeCalc(0.03));
            AddStrategyByIndicator(new SpikeVolumeShapeCalc(0.025));
            AddStrategyByIndicator(new WaysShapeCalc(new ShapeScanner()));
            AddStrategyByIndicator(new TripleShapeCalc(new TripleShapeScanner()));
            AddStrategyByIndicator(new MovingAvgCalc());
            AddStrategyByIndicator(new MovingAverageCalc());

            AddMixedIndicators(new SimpleShapeCalc(), new BasicSignalCalc(new MACDSignal()));

            const double BUYMARGINPERCENT = 0.3; // 门限
            const double SELLMARGINPERCENT = 0.6;
            AddStrategyByIndicator(new VolumeCalc(BUYMARGINPERCENT, SELLMARGINPERCENT));

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
            _AllStrategies.Add(strategy.Name, strategy);
        }

        protected void AddStrategyByIndicator(IIndicatorCalc calc)
        {
            AddStrategy(new StrategyIndicator(calc));
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
