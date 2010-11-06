using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer.DB
{
    interface IDataChecker
    {
        void Check(ICustomLog log);
    }
}
