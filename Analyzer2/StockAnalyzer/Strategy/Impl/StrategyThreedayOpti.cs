using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Strategy.Rise;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Impl
{
    // 策略描述：连续两天下跌，第三天开盘买入；连续两天上涨，第三天开盘卖出； 
    class StrategyThreedayOpti : IFinanceStrategy
    {
        public override string Name
        {
            get
            {
                return "3DayOpti: " + _Judger.Name;
            }
        }

        public StrategyThreedayOpti(IStockJudger judger)
        {
            _Judger = judger;
        }

        // 得到操作指令
        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData curProp = _StockHistory.GetStock(day);
            IStockData stockYesterdayProp = _StockHistory.GetPrevDayStock(day);

            DateTime prevDate = _StockHistory.GetPrevDay(day);
            IStockData stockprevProp = _StockHistory.GetPrevDayStock(prevDate);
            DateTime prevNextDate = _StockHistory.GetPrevDay(prevDate);

            if (!CheckStock(curProp, day) || !CheckStock(stockYesterdayProp, prevDate)
                || !CheckStock(stockprevProp, prevNextDate))
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            if (_Judger.FulFil(stockprevProp, stockYesterdayProp, curProp))
            {
                if (_StockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.StartPrice, _StockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper);
                    return opers;
                }
            }
            else if (_Judger.ReverseFulFil(stockprevProp, stockYesterdayProp, curProp))
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
            else
            {
                return null;
            }

            return null;
        }

        private IStockJudger _Judger;
    }
}
