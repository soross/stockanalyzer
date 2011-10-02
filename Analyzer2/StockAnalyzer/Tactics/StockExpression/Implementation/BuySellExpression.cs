using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Tactics.StockExpression.Implementation
{
    class BuySellExpression : IStockExpression
    {

        #region IStockExpression Members

        public void InitParam(Dictionary<string, object> paramMap)
        {
            throw new NotImplementedException();
        }

        public OperType GetOper(DateTime dt)
        {
            throw new NotImplementedException();
        }

        public string ExpressionName
        {
            get { throw new NotImplementedException(); }
        }
        
        public Stock.IStockHistory History
        {
            set { throw new NotImplementedException(); }
        }

        public void Run()
        {
        }
        
        public Stock.IStockValues TotalAccountValue
        {
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
}
