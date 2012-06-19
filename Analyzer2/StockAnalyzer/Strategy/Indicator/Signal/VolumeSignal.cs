using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class VolumeSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false ;
            }

            Averager_.AddVal(sd.Amount);
            if (!Averager_.IsEnough())
            {
                return false;
            }

            double avgMinAmount = Averager_.GetValue() * (1 - BuyMargin_);
            double avgMaxAmount = Averager_.GetValue() * (1 + SellMargin_);

            if (sd.Amount < avgMinAmount)
            {
                // 成交地量，买入 
                TodayOper_ = OperType.Buy;
            }
            else if (sd.Amount > avgMaxAmount)
            {
                // 成交天量，卖出
                TodayOper_ = OperType.Sell;
            }
            else
            {
                TodayOper_ = OperType.NoOper;
            }

            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "Volume " + BuyMargin_.ToString() + ": " + SellMargin_.ToString();
        }

        #endregion

        public VolumeSignal(double buyMargin, double sellMargin)
        {
            BuyMargin_ = buyMargin;
            SellMargin_ = sellMargin;
        }

        ValueAverager<double> Averager_ = new ValueAverager<double>(AVERAGEDAYS);

        const int AVERAGEDAYS = 3;
        double BuyMargin_;
        double SellMargin_;

        OperType TodayOper_;
    }
}
