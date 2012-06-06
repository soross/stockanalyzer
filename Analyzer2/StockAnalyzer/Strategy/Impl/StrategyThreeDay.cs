using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using FinanceAnalyzer.Strategy.Judger;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Impl
{
    // Given the previous three days, use the specific judger to generate the trade rule 
    public class StrategyThreeDay : IFinanceStrategy
    {
        public override string Name
        {
            get
            {
                return "3Day: " + Judger_.Name;
            }
        }

        public StrategyThreeDay(IStockJudger judger)
        {
            Judger_ = judger;
        }

        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData curProp = stockHistory.GetStock(day);
            IStockData stockYesterdayProp = stockHistory.GetPrevDayStock(day);

            DateTime prevDate = stockHistory.GetPreviousDay(day);
            IStockData stockprevProp = stockHistory.GetPrevDayStock(prevDate);
            DateTime prevNextDate = stockHistory.GetPreviousDay(prevDate);

            if (!CheckStock(curProp, day) || !CheckStock(stockYesterdayProp, prevDate)
                || !CheckStock(stockprevProp, prevNextDate))
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            if (Judger_.FulFil(stockprevProp, stockYesterdayProp, curProp))
            {
                if (stockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.EndPrice, stockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper);
                    return opers;
                }
            }
            else if (Judger_.ReverseFulFil(stockprevProp, stockYesterdayProp, curProp))
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

        private IStockJudger Judger_;
    }
}
