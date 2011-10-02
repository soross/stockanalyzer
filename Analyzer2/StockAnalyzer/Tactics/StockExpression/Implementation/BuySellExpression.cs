using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Tactics.StockExpression.Implementation
{
    class BuySellExpression : IStockExpression
    {

        #region IStockExpression Members

        public void InitParam(Dictionary<string, object> paramMap)
        {
            throw new NotImplementedException();
        }

        public Stock.OperType GetOper(DateTime dt)
        {
            throw new NotImplementedException();
        }

        public string ExpressionName
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
