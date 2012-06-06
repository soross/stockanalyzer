using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class FibonacciCalc : BasicIndicatorCalc
    {
        public override void Calc(Stock.IStockHistory hist)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { return "Fibonacci"; }
        }
    }
}
