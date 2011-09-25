using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Strategy.Rise;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Impl
{
    // �������������������µ��������쿪�����룻�����������ǣ������쿪�������� 
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

        // �õ�����ָ��
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
            if (_Judger.FulFil(stockprevProp, stockYesterdayProp, curProp))
            {
                if (stockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.StartPrice, stockHolder.StockCount(), OperType.Sell);
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
