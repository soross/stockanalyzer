using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Strategy.Indicator;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Impl
{
    // 基于指标的计算方式 
    class StrategyIndicator : IFinanceStrategy
    {
        public StrategyIndicator(IIndicatorCalc calc)
        {
            _Calc = calc;
        }

        public override ICollection<StockOper> GetOper(DateTime dt, IAccount account)
        {
            DateTime yesterday = stockHistory.GetPreviousDay(dt);

            IStockData curProp = stockHistory.GetStock(dt);
            if (curProp == null)
            {
                return null;
            }

            OperType operType = _Calc.MatchSignal(dt, yesterday);
            if (operType == OperType.Buy)
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    curProp.EndPrice);
                if (stockCount <= 0)
                {
                    return null;
                }

                ICollection<StockOper> opers = new List<StockOper>();
                StockOper oper = new StockOper(curProp.EndPrice, stockCount, OperType.Buy);
                opers.Add(oper);
                return opers; // 买入
            }
            else if (operType == OperType.Sell)
            {
                if (!stockHolder.HasStock())
                {
                    return null;
                }

                ICollection<StockOper> opers = new List<StockOper>();
                StockOper oper = new StockOper(curProp.EndPrice, stockHolder.StockCount(), OperType.Sell);
                opers.Add(oper);
                return opers;
            }
            return null;
        }

        public override string Name
        {
            get
            {
                return _Calc.Name;
            }
        }

        protected override void HolderInit()
        {
            _Calc.Calc(stockHistory);
        }

        IIndicatorCalc _Calc;
    }
}
