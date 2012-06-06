using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class FibonacciCalc : BasicIndicatorCalc
    {
        public override void Calc(IStockHistory hist)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { return "Fibonacci"; }
        }
    }
}
