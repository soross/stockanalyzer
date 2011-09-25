using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Impl
{
    class StrategyVolume : IFinanceStrategy
    {
        public StrategyVolume(double buyMargin, double sellMargin)
        {
            _BuyMargin = buyMargin;
            _SellMargin = sellMargin;
        }

        public override ICollection<StockOper> GetOper(DateTime day, IAccount account)
        {
            IStockData curProp = stockHistory.GetStock(day);
            if (!CheckStock(curProp, day))
            {
                return null;
            }

            if (!_Averager.IsEnough())
            {
                _Averager.AddVal(curProp.Amount);
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            double avgMinAmount = _Averager.GetValue() * (1 - _BuyMargin);
            double avgMaxAmount = _Averager.GetValue() * (1 + _SellMargin);

            _Averager.AddVal(curProp.Amount);

            if (curProp.Amount < avgMinAmount)
            {
                // 成交地量，买入 
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    curProp.EndPrice);
                if (stockCount > 0)
                {
                    StockOper oper = new StockOper(curProp.EndPrice, stockCount, OperType.Buy);
                    opers.Add(oper);
                    return opers;
                }
            }
            else if (curProp.Amount > avgMaxAmount)
            {
                // 成交天量，卖出
                if (stockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.EndPrice, stockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper);
                    return opers;
                }
            }
            
            return null;
        }

        public override string Name
        {
            get { return "Volume: " + _BuyMargin.ToString() + ":" + _SellMargin.ToString(); }
        }

        ValueAverager<double> _Averager = new ValueAverager<double>(AVERAGEDAYS);

        const int AVERAGEDAYS = 3;
        double _BuyMargin;
        double _SellMargin;
        const double BUYMARGINPERCENT = 0.5; // 门限
        const double SELLMARGINPERCENT = 0.6;
    }
}
