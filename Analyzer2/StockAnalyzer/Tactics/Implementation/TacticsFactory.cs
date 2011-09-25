using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Tactics.StockExpression;

namespace FinanceAnalyzer.Tactics.Implementation
{
    class TacticsFactory
    {
        public void Init(IStockHistory hist)
        {
            IStockExpression expr = StockExpressionFactory.Instance().GetExpression(StockExpressionNames.THREEDAYS);
            ITactics tac = new CommonTactics();
            tac.CurrentExpression = expr;
            tac.History = hist;

            allTactics.Add(tac);
        }

        public void Run()
        {
            foreach (var tac in allTactics)
            {
                tac.Run();
            }
        }

        List<ITactics> allTactics = new List<ITactics>();
    }
}
