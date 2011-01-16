using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.KDJ
{
    interface IKdjStorage
    {
        double GetK(DateTime dt);
        double GetD(DateTime dt);
        double GetJ(DateTime dt);
    }
}
