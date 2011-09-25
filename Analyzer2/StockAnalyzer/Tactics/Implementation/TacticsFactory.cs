using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Tactics.StockExpression.Implementation;
using FinanceAnalyzer.Tactics.StockExpression;

namespace FinanceAnalyzer.Tactics.Implementation
{
    class TacticsFactory
    {
        public void Init()
        {
            IStockExpression tac = StockExpressionFactory.Instance().GetExpression("Three Days");

        }

        List<ITactics> allTactics = new List<ITactics>();
    }
}
