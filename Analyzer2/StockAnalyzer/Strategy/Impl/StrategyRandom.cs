using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Impl
{
    // 随机生成买、卖、无操作 
    class StrategyRandom : IFinanceStrategy
    {
        public override string Name
        {
            get
            {
                return "Random";
            }
        }

        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            Random rnd = new Random(unchecked((int)(DateTime.Now.Ticks)));

            double val = rnd.NextDouble();

            IStockData stockProp = stockHistory.GetStock(day);
            if (stockProp == null)
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            if (val < (1.0 / 3))
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    stockProp.StartPrice);
                StockOper oper = new StockOper(stockProp.StartPrice, stockCount, OperType.Buy);
                opers.Add(oper);
            }
            else if (val > (2.0 / 3))
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    stockProp.EndPrice);
                StockOper oper = new StockOper(stockProp.EndPrice, stockCount, OperType.Sell);
                opers.Add(oper);
            }
            else
            {
            }

            return opers;
        }
    }
}
