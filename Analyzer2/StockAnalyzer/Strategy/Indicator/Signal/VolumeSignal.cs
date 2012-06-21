using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    public class VolumeSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }
                        
            if (!Averager_.IsEnough())
            {
                Averager_.AddVal(sd.Amount);
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

            Averager_.AddVal(sd.Amount);
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
