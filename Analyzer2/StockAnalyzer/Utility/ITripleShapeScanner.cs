using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Utility
{
    interface ITripleShapeScanner
    {
        OperType Analyse(IStockData prevStock, IStockData stock, IStockData nextStock);
    }
}
