﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Tactics.StockExpression;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Tactics.Implementation
{
    class CommonTactics : ITactics
    {
        #region ITactics Members

        public IStockHistory History
        {
            set { History_ = value; }
        }

        public IStockExpression CurrentExpression
        {
            set { Expression_ = value; }
        }

        public IStockValues TotalAccountValue
        {
            get { return StockValue_; }
        }

        public void Run()
        {
            IStockExpression expr = StockExpressionFactory.Instance().GetExpression(
                StockExpressionNames.THREEDAYS);

            DateTime curDate = History_.MinDate;
            while (curDate < History_.MaxDate)
            {


                curDate = DateFunc.GetNextWorkday(curDate);
            }
        }

        #endregion

        IStockHistory History_;
        IStockExpression Expression_;

        IStockValues StockValue_ = new StockValues();
    }
}
