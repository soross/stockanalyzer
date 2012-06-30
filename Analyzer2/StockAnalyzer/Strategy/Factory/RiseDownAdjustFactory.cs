using FinanceAnalyzer.Strategy.Indicator;
using FinanceAnalyzer.Strategy.Indicator.Signal;

namespace FinanceAnalyzer.Strategy.Factory
{
    class RiseDownAdjustFactory : StrategyFactory
    {
        public override void Init()
        {
            for (double i = 0.03; i < 0.4; i += 0.01)
            {
                AddStrategyBySignal(new RiseDownSignal(i));
            }

            AddStrategyBySignal(new HoldSignal());
        }
    }
}
