using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class MovingAverageCalc : HistoricalValuesCalc
    {
        public MovingAverageCalc()
        {
            _Prediction.LongDays = 10;
            _Prediction.ShortDays = 5;
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

                _Prediction.AddPrice(stock.EndPrice);

                if (!_Prediction.IsCountEnough())
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                // 长周期日线和短周期日线
                double longEMA = _Prediction.GetLongAverage();
                double shortEMA = _Prediction.GetShortAverage();

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

            if ((_LastOperDate != DateTime.MinValue) && ((dt - _LastOperDate).Days <= IGNOREDAYS))
            {
                return OperType.NoOper;
            }

            if ((this.GetIndicatorValue(dt) > 0) && (this.GetIndicatorValue(prev) < 0))
            {
                _LastOperDate = dt;
                return OperType.Buy;
            }
            else if ((this.GetIndicatorValue(dt) < 0) && (this.GetIndicatorValue(prev) > 0))
            {
                _LastOperDate = dt;
                return OperType.Sell;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        DateTime _LastOperDate = DateTime.MinValue;
        MovingAveragePrediction _Prediction = new MovingAveragePrediction();
        const int IGNOREDAYS = 3;
    }
}
