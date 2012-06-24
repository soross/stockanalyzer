using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class MACDSignal : ISignalCalculator
    {

        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            currentStatus_ = Alg_.AddValue(sd.EndPrice);

            if (currentStatus_)
            {
                macdValues_.AddValue(Alg_.GetMacd());
            }

            return true;
        }

        public OperType GetSignal()
        {
            if (!currentStatus_)
            {
                return OperType.NoOper;
            }

            if ((macdValues_.CurrentValue == double.NaN) 
                || (macdValues_.PreviousValue == double.NaN))
            {
                return OperType.NoOper;
            }

            if ((macdValues_.CurrentValue > 0) && (macdValues_.PreviousValue < 0))
            {
                return OperType.Buy;
            }
            else if ((macdValues_.CurrentValue < 0) && (macdValues_.PreviousValue > 0))
            {
                return OperType.Sell;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        public string GetName()
        {
            return "MACD";
        }

        #endregion

        bool currentStatus_;
        TwoDayValues macdValues_ = new TwoDayValues();
        MACDAlgorithm Alg_ = new MACDAlgorithm();

        class TwoDayValues
        {
            public void AddValue(double val)
            {
                if (currentValue_ != double.NaN)
                {
                    previousValue_ = currentValue_;                    
                }

                currentValue_ = val;
            }

            public double CurrentValue
            {
                get
                {
                    return currentValue_;
                }
            }

            public double PreviousValue
            {
                get
                {
                    return previousValue_;
                }
            }

            double currentValue_ = double.NaN;
            double previousValue_ = double.NaN;
        }
    }
}
