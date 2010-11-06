using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.KDJ
{
    interface IKdjCalculator
    {
        void Calc(IStockHistory hist);

        IKdjStorage GetStorage();
    }
}
