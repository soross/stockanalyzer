using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Indicator;
using FinanceAnalyzer.Strategy.Impl;
using FinanceAnalyzer.Strategy.Indicator.Signal;

namespace FinanceAnalyzer.Strategy.Factory
{
    class KdAdjustFactory : StrategyFactory
    {
        public override void Init()
        {
            for (double i = 15; i < 35; i += 2)
            {
                for (double j = 65; j < 85; j += 2)
                {
                    AddStrategy(new StrategyKD(i, j));
                }
            }

            AddStrategyByIndicator(new BasicSignalCalc(new HoldSignal()));
        }
    }
}
