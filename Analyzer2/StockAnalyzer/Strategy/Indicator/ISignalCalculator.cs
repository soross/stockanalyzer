using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    interface ISignalCalculator
    {
        void AddStock(IStockData sd);

        OperType GetSignal();
    }
}
