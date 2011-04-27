using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business.Shape;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Impl
{
    class StrategyVolumeOptim : IFinanceStrategy
    {
        public StrategyVolumeOptim(double buyMargin, double sellMargin)
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

            _Averager.AddVal(curProp.Amount);
            _SlopeCalc.AddVal(curProp.EndPrice); // 计算收盘价斜率 

            if (!_Averager.IsEnough() || !_SlopeCalc.IsEnough())
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            double avgMinAmount = _Averager.GetValue() * (1 - _BuyMargin);
            double avgMaxAmount = _Averager.GetValue() * (1 + _SellMargin);

            ShapeJudger judger = new ShapeJudger(curProp);
            //if ((curProp.Amount < avgMinAmount) && (judger.IsCross() && _SlopeCalc.IsDownPeriod()))
            if ((curProp.Amount < avgMinAmount) && ShapeJudger.IsCross(curProp))
            {
                // 成交地量，十字星，买入 
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    curProp.EndPrice);
                if (stockCount > 0)
                {
                    StockOper oper = new StockOper(curProp.EndPrice, stockCount, OperType.Buy);
                    opers.Add(oper);
                    return opers;
                }
            }
            else if ((curProp.Amount > avgMaxAmount) && _SlopeCalc.IsRisePeriod())
            {
                // 上涨过程，成交天量，卖出
                if (stockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.EndPrice, stockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper);
                    return opers;
                }
            }

            if (judger.IsReverseT())
            {
                // 倒T型，卖出
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
            get { return "VolumeOptim: " + _BuyMargin + ", " + _SellMargin; }
        }

        ValueAverager<double> _Averager = new ValueAverager<double>(AVERAGEDAYS);
        SlopeCalculator _SlopeCalc = new SlopeCalculator(CURVEDAYS);

        const int AVERAGEDAYS = 3;
        const int CURVEDAYS = 5;

        double _BuyMargin;
        double _SellMargin;
    }
}
