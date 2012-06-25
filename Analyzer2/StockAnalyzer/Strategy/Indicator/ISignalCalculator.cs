using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    public interface ISignalCalculator
    {
        bool AddStock(IStockData sd);

        OperType GetSignal();

        string GetName();
    }
}
