using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;

namespace FinanceAnalyzer.Tactics.StockExpression.Implementation
{
    class ThreeDays : IStockExpression
    {
        public ThreeDays()
        {
            IncreaseExpr_ = new Expression(IncreaseExpressionStr_);
            DecreaseExpr_ = new Expression(DecreaseexpressionStr_);
        }

        #region IStockExpression Members

        public void InitParam(Dictionary<string, object> paramMap)
        {
            IncreaseExpr_.Parameters = paramMap;
            DecreaseExpr_.Parameters = paramMap;
        }

        public OperType GetOper(DateTime dt)
        {
            IncreaseExpr_.Evaluate();
            DecreaseExpr_.Evaluate();

            if (IncreaseExpr_.Evaluate().Equals(1))
            {
                return OperType.Sell;
            }

            if (DecreaseExpr_.Evaluate().Equals(1))
            {
                return OperType.Buy;
            }

            return OperType.NoOper;
        }

        #endregion

        string IncreaseExpressionStr_ = "if(([BeforeYesterday] < [Yesterday]) && ([Yesterday] < [Today]), 1, 0)";
        string DecreaseexpressionStr_ = "if(([BeforeYesterday] > [Yesterday]) && ([Yesterday] > [Today]), 1, 0)";

        Expression IncreaseExpr_;
        Expression DecreaseExpr_;
    }
}
