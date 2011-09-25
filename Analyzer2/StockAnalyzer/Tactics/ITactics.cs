using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Tactics.StockExpression;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Tactics
{
    interface ITactics
    {
        IStockHistory History
        {
            set;
        }

        IStockExpression CurrentExpression
        {
            set;
        }

        IStockValues TotalAccountValue
        {
            get;
        }

        void Run();
    }
}
