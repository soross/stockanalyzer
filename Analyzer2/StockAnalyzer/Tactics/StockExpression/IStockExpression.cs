using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;

namespace FinanceAnalyzer.Tactics.StockExpression
{
    interface IStockExpression
    {
        void InitParam(Dictionary<string, object> paramMap);
        OperType GetOper(DateTime dt);
        string ExpressionName
        {
            get;
        }
    }
}
