using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.KDJ
{
    interface IKdjCalculator
    {
        void Calc(IStockHistory hist);

        IKdjStorage GetStorage();
    }
}
