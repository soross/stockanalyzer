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
        IStockHistory History
        {
            set;
        }

        void Init();

        void InitParam(Dictionary<string, object> paramMap);
        OperType GetOper(DateTime dt);
        string ExpressionName
        {
            get;
        }
    }
}
