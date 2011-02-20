using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Indicator;
using FinanceAnalyzer.Strategy.Impl;

namespace FinanceAnalyzer.Strategy.Factory
{
    class RiseDownAdjustFactory : StrategyFactory
    {
        public override void Init()
        {
            for (double i = 0.03; i < 0.4; i += 0.01)
            {
                AddStrategy(new StrategyIndicator(new RiseDownCalc(i)));
            }

            AddStrategyByIndicator(new HoldCalc());
        }
    }
}
