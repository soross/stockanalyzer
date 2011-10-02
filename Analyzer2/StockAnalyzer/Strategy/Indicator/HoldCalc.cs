using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class HoldCalc : IIndicatorCalc
    {
        public string Name
        {
            get { return "Hold"; }
        }

        public void Calc(IStockHistory hist)
        {
            _MinDate = hist.MinDate;
        }

        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (dt == _MinDate)
            {
                return OperType.Buy;
            }
            else if (DateFunc.IsHoliday(_MinDate) && (DateFunc.GetPreviousWorkday(dt) <= _MinDate))
            {
                return OperType.Buy;
            }
            else if (prev < _MinDate)
            {
                return OperType.Buy;
            }

            return OperType.NoOper;
        }

        DateTime _MinDate;
    }
}
