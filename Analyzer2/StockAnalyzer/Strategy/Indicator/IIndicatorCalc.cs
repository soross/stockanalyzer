using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Strategy.Indicator
{
    // 指标计算器
    public interface IIndicatorCalc
    {
        void Calc(IStockHistory hist);

        OperType MatchSignal(DateTime dt, DateTime prev);

        string Name
        {
            get;
        }
    }
}
