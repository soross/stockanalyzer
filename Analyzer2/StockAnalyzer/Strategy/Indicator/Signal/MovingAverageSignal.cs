using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class MovingAverageSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            prediction.AddPrice(sd.EndPrice);

            if (!prediction.IsCountEnough())
            {
                return false;
            }

            // 长周期日线和短周期日线
            double longEMA = prediction.GetLongAverage();
            double shortEMA = prediction.GetShortAverage();

            double diff = shortEMA - longEMA; // 短周期均值-长周期均值

            if (double.IsNaN(prevMA))
            {
                return false;
            }

            TodayOper_ = OperType.NoOper;
            if (prevMA < 0)
            {
                if (prediction.CalcNextPredictionValue() > 0)
                {
                    TodayOper_ = OperType.Buy;
                }
            }
            else if (prevMA > 0)
            {
                if (prediction.CalcNextPredictionValue() < 0)
                {
                    TodayOper_ = OperType.Sell;
                }
            }

            prevMA = diff;
            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "MA Signal";
        }

        #endregion

        public MovingAverageSignal()
        {
            prediction.LongDays = 10;
            prediction.ShortDays = 5;
        }

        MovingAveragePrediction prediction = new MovingAveragePrediction();

        double prevMA = double.NaN;

        OperType TodayOper_;
    }
}
