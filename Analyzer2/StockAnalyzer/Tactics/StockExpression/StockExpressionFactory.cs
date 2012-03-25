using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Tactics.StockExpression.Implementation;

namespace FinanceAnalyzer.Tactics.StockExpression
{
    /// <summary>
    /// Generate Stock expression
    /// (Singleton)
    /// </summary>
    class StockExpressionFactory
    {
        private StockExpressionFactory()
        {
            Init();
        }

        static StockExpressionFactory instance_ = new StockExpressionFactory();

        public static StockExpressionFactory Instance()
        {
            return instance_;
        }

        void Init()
        {
            // Add all expressions
            AddExpression(new ThreeDays());
        }

        /// <summary>
        /// Get expression by it's name
        /// </summary>
        /// <param name="expressionName"></param>
        /// <returns></returns>
        public IStockExpression GetExpression(string expressionName)
        {
            if (AllExpressions_.ContainsKey(expressionName))
            {
                return AllExpressions_[expressionName];
            }

            return null;
        }

        void AddExpression(IStockExpression expr)
        {
            AllExpressions_.Add(expr.ExpressionName, expr);
        }

        Dictionary<string, IStockExpression> AllExpressions_ = new Dictionary<string, IStockExpression>();
    }
}
