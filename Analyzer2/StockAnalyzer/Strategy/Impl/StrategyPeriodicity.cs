using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Impl
{
    class StrategyPeriodicity : IFinanceStrategy
    {
        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData curStockProp = stockHistory.GetStock(day);
            if (curStockProp == null)
            {
                return null;
            }

            if (account.BankRoll < CASHPERTRANSACTION)
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();

            if (IsBuyDays(day))
            {
                int stockCount = Transaction.GetCanBuyStockCount(CASHPERTRANSACTION,
                        curStockProp.StartPrice); // 开盘买入 
                StockOper oper = new StockOper(curStockProp.StartPrice, stockCount, OperType.Buy);
                opers.Add(oper);

                return opers;
            }

            return null;
        }

        protected override void HolderInit()
        {
            DateTime startDate = stockHistory.MinDate;
            while (startDate < stockHistory.MaxDate)
            {
                _AllBuyDays.Add(startDate);

                startDate = startDate.AddMonths(1); // 每月买入
                if (DateFunc.IsHoliday(startDate))
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                }
            }
        }

        public override string Name
        {
            get { return "Periodicity"; }
        }

        private bool IsBuyDays(DateTime dt)
        {
            return _AllBuyDays.Contains(dt);
        }

        List<DateTime> _AllBuyDays = new List<DateTime>();

        const double CASHPERTRANSACTION = 5000; // 每次买入X元 
    }
}
