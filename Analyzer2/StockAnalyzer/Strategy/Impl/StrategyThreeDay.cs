using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using FinanceAnalyzer.Strategy.Rise;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Impl
{
    // Given the previous three days, use the specific judger to generate the trade rule 
    public class StrategyThreeDay : IFinanceStrategy
    {
        public override string Name
        {
            get
            {
                return "3Day: " + _Judger.Name;
            }
        }

        public StrategyThreeDay(IStockJudger judger)
        {
            _Judger = judger;
        }

        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData curProp = stockHistory.GetStock(day);
            IStockData stockYesterdayProp = stockHistory.GetPrevDayStock(day);

            DateTime prevDate = stockHistory.GetPrevDay(day);
            IStockData stockprevProp = stockHistory.GetPrevDayStock(prevDate);
            DateTime prevNextDate = stockHistory.GetPrevDay(prevDate);

            if (!CheckStock(curProp, day) || !CheckStock(stockYesterdayProp, prevDate)
                || !CheckStock(stockprevProp, prevNextDate))
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            if (_Judger.FulFil(stockprevProp, stockYesterdayProp, curProp))
            {
                if (stockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.EndPrice, stockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper);
                    return opers;
                }
            }
            else if (_Judger.ReverseFulFil(stockprevProp, stockYesterdayProp, curProp))
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    curProp.EndPrice);
                if (stockCount > 0)
                {
                    StockOper oper = new StockOper(curProp.EndPrice, stockCount, OperType.Buy);
                    opers.Add(oper);
                    return opers;
                }                
            }

            return null;
        }

        protected override void HolderInit()
        {
        }

        private IStockJudger _Judger;
    }
}
