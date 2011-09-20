using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Tactics.StockExpression
{
    interface IStockExpression
    {
        void InitParam(Dictionary<string, object> paramMap);
        OperType GetOper(DateTime dt);
    }
}
