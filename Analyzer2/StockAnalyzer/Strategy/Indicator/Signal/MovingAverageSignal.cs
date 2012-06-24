using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    /// <summary>
    /// Calculate MA5 and MA10
    /// MA5 up cross MA10 -> Buy
    /// MA5 down cross MA10 -> Sell
    /// </summary>
    class MovingAverageSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            prediction_.AddPrice(sd.EndPrice);

            if (!prediction_.IsCountEnough())
            {
                return false;
            }

            // 长周期日线和短周期日线
            double longEMA = prediction_.GetLongAverage();
            double shortEMA = prediction_.GetShortAverage();

            double diff = shortEMA - longEMA; // 短周期均值-长周期均值

            if (double.IsNaN(previousMA_))
            {
                previousMA_ = diff;
                return false;
            }

            TodayOper_ = OperType.NoOper;
            if (previousMA_ < 0)
            {
                if (prediction_.CalcNextPredictionValue() > 0)
                {
                    TodayOper_ = OperType.Buy;
                }
            }
            else if (previousMA_ > 0)
            {
                if (prediction_.CalcNextPredictionValue() < 0)
                {
                    TodayOper_ = OperType.Sell;
                }
            }

            previousMA_ = diff;
            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "MA";
        }

        #endregion

        public MovingAverageSignal()
        {
            prediction_.LongDays = 10;
            prediction_.ShortDays = 5;
        }

        MovingAveragePrediction prediction_ = new MovingAveragePrediction();

        double previousMA_ = double.NaN;

        OperType TodayOper_;
    }
}
