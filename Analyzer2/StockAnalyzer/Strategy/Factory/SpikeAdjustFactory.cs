using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Indicator.Shape;
using FinanceAnalyzer.Strategy.Indicator;

namespace FinanceAnalyzer.Strategy.Factory
{
    class SpikeAdjustFactory : StrategyFactory
    {
        public override void Init()
        {
            for (double i = 0.005; i < 0.06; i += 0.005)
            {
                AddStrategyByIndicator(new SpikeShapeCalc(i)); 
            }

            AddStrategyByIndicator(new HoldCalc());
        }
    }
}
