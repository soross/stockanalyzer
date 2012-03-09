using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class MovingAverageCalc : HistoricalValuesCalc
    {
        public MovingAverageCalc()
        {
            Prediction_.LongDays = 10;
            Prediction_.ShortDays = 5;
        }

        public override string Name
        {
            get { return "MovAvg"; }
        }

        public override void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                if (stock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                Prediction_.AddPrice(stock.EndPrice);

                if (!Prediction_.IsCountEnough())
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                // 长周期日线和短周期日线
                double longEMA = Prediction_.GetLongAverage();
                double shortEMA = Prediction_.GetShortAverage();

                double diff = shortEMA - longEMA; // 短周期均值-长周期均值

                _DateIndicators.Add(startDate, diff);

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        public override OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (double.IsNaN(GetIndicatorValue(dt)) || double.IsNaN(GetIndicatorValue(prev)))
            {
                return OperType.NoOper;
            }

            if ((LastOperDate_ != DateTime.MinValue) && ((dt - LastOperDate_).Days <= IGNOREDAYS))
            {
                return OperType.NoOper;
            }

            if ((this.GetIndicatorValue(dt) > 0) && (this.GetIndicatorValue(prev) < 0))
            {
                LastOperDate_ = dt;
                return OperType.Buy;
            }
            else if ((this.GetIndicatorValue(dt) < 0) && (this.GetIndicatorValue(prev) > 0))
            {
                LastOperDate_ = dt;
                return OperType.Sell;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        DateTime LastOperDate_ = DateTime.MinValue;
        MovingAveragePrediction Prediction_ = new MovingAveragePrediction();
        const int IGNOREDAYS = 10;
    }
}
