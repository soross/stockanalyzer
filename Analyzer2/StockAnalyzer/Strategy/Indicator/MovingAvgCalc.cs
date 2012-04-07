using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using System.Diagnostics;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator
{
    /// <summary>
    /// Calculate MA5 and MA10
    /// MA5 up cross MA10 -> Buy
    /// MA5 down cross MA10 -> Sell
    /// </summary>
    class MovingAvgCalc : BasicIndicatorCalc
    {
        public MovingAvgCalc()
        {
        }

        public override string Name
        {
            get { return "MA"; }
        }

        public override void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            MovingAveragePrediction prediction = new MovingAveragePrediction();
            prediction.LongDays = 10;
            prediction.ShortDays = 5;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                if (stock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                prediction.AddPrice(stock.EndPrice);

                if (!prediction.IsCountEnough())
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                // 长周期日线和短周期日线
                double longEMA = prediction.GetLongAverage();
                double shortEMA = prediction.GetShortAverage();

                double diff = shortEMA - longEMA; // 短周期均值-长周期均值

                DateIndicators_.Add(startDate, diff);
                
                CalcIndicators(hist, stock, prediction);

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        void CalcIndicators(IStockHistory hist, IStockData stock, MovingAveragePrediction prediction)
        {
            DateTime localDate = DateFunc.ConvertToLocal(stock.TradeDate);
            DateTime prev = hist.GetPreviousDay(localDate);

            double prevValue = GetIndicatorValue(prev);

            if (double.IsNaN(prevValue))
            {
                return ;
            }

            if (prevValue < 0)
            {
                if (prediction.CalcNextPredictionValue() > 0)
                {
                    // Up cross
                    DateToOpers_.Add(localDate, OperType.Buy);
                }
            }

            if (prevValue > 0)
            {
                if (prediction.CalcNextPredictionValue() < 0)
                {
                    // Down cross
                    DateToOpers_.Add(localDate, OperType.Sell);
                }
            }
        }

        // 得到某天的指标值
        double GetIndicatorValue(DateTime dt)
        {
            if (DateIndicators_.ContainsKey(dt))
            {
                return DateIndicators_[dt];
            }
            else
            {
                return double.NaN; // 默认返回NaN 
            }
        }

        Dictionary<DateTime, double> DateIndicators_ = new Dictionary<DateTime, double>();
    }
}
