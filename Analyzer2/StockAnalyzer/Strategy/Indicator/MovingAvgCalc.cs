using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class MovingAvgCalc : IIndicatorCalc
    {
        public string Name
        {
            get { return "MA"; }
        }

        public void Calc(IStockHistory hist)
        {

        }

        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
            return OperType.NoOper;
        }
    }
}
