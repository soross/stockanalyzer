using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

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

        public void Run()
        {
            DateTime curDate = History.MinDate;
            while (curDate < History.MaxDate)
            {
                DateTime prevDate = History.GetPreviousDay(curDate);
                DateTime prev2Date = History.GetPreviousDay(prevDate);

                Dictionary<string, object> stockValues = new Dictionary<string, object>();
                stockValues.Add("BeforeYesterday", History.GetStock(prev2Date).EndPrice);
                stockValues.Add("Yesterday", History.GetStock(prevDate).EndPrice);
                stockValues.Add("Today", History.GetStock(curDate).EndPrice);

                InitParam(stockValues);



                curDate = DateFunc.GetNextWorkday(curDate);
            }
        }

        public IStockValues TotalAccountValue
        {
            get;
            set;
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

        public string ExpressionName
        {
            get
            {
                return StockExpressionNames.THREEDAYS;
            }
        }

        public IStockHistory History
        {
            get;
            set;
        }

        #endregion

        const string IncreaseExpressionStr_ = "if(([BeforeYesterday] < [Yesterday]) && ([Yesterday] < [Today]), 1, 0)";
        const string DecreaseexpressionStr_ = "if(([BeforeYesterday] > [Yesterday]) && ([Yesterday] > [Today]), 1, 0)";

        static Expression IncreaseExpr_;
        static Expression DecreaseExpr_;
    }
}
