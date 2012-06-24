using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    abstract class SignalCalculator : ISignalCalculator
    {
        #region ISignalCalculator Members

        abstract public bool AddStock(IStockData sd);

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        abstract public string GetName();

        #endregion

        protected OperType TodayOper_;
    }
}
