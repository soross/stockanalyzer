using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Impl
{
    // 量价背离，未完成
    class StrategyPriceAmountDeviation : IFinanceStrategy
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

            // TODOs

            return null;
        }

        public override string Name
        {
            get
            {
                return "PriceAmountDeviation";
            }
        }
    }
}
