using System;
using System.Collections.Generic;
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

        void Run();

        void InitParam(Dictionary<string, object> paramMap);
        OperType GetOper(DateTime dt);
        string ExpressionName
        {
            get;
        }

        IStockValues TotalAccountValue
        {
            set;
        }
    }
}
