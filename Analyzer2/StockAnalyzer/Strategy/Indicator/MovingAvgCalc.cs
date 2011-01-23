using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using System.Diagnostics;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class MovingAvgCalc : HistoricalValuesCalc
    {
        public MovingAvgCalc()
        {
            _Prediction.LongDays = 10;
            _Prediction.ShortDays = 5;
        }

        public override string Name
        {
            get { return "MA"; }
        }

        public override void Calc(IStockHistory hist)
        {
            _History = hist;

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

            if (this.GetIndicatorValue(prev) < 0)
            {
                return OperType.Buy;
            }
            else if (this.GetIndicatorValue(prev) > 0)
            {
                return OperType.Sell;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        IStockHistory _History;
        MovingAveragePrediction _Prediction = new MovingAveragePrediction();
    }
}
