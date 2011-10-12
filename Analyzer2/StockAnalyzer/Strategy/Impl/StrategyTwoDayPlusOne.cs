using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Strategy.Judger;

namespace FinanceAnalyzer.Strategy.Impl
{
    class StrategyTwoDayPlusOne : IFinanceStrategy
    {
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

            // IsRise? Condition questionable. 
            if (
                StockJudger.IsRise(stockYesterdayProp, stockprevProp)
                && 
                StockJudger.IsUp(stockYesterdayProp)
                && StockJudger.IsUp(stockprevProp))
            {
                if (stockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.StartPrice, stockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper);
                    return opers;
                }
            }
            else if (
                !StockJudger.IsRise(stockYesterdayProp, stockprevProp)
                && 
                !StockJudger.IsUp(stockYesterdayProp)
                && !StockJudger.IsUp(stockprevProp))
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    curProp.StartPrice);
                if (stockCount > 0)
                {
                    StockOper oper = new StockOper(curProp.StartPrice, stockCount, OperType.Buy);
                    opers.Add(oper);
                    return opers;
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "2Day Plus"; }
        }
    }
}
