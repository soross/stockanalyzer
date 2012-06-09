using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using System.Linq;
using System.Diagnostics;
using FinanceAnalyzer.Strategy.Indicator;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    // 参考：http://en.wikipedia.org/wiki/MACD
    // 传统MACD计算方式 
    class MacdCalculator : HistoricalValuesCalc
    {
        public override string Name
        {
            get { return "MACD"; }
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

                AddTodayPrice(stock.EndPrice);

                if (!IsCountEnough())
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                Debug.Assert(LongPrices_.Count == LONGDAYS);
                Debug.Assert(ShortPrices_.Count == SHORTDAYS);

                // 计算长周期和短周期均值
                double longEMA = LongPrices_.Average();
                double shortEMA = ShortPrices_.Average();

                double diff = shortEMA - longEMA; // 短周期均值-长周期均值

                DiffPrices_.Add(diff);
                if (DiffPrices_.Count < MIDDAYS)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }
                else if (DiffPrices_.Count > MIDDAYS)
                {
                    DiffPrices_.RemoveAt(0);
                }
                Debug.Assert(DiffPrices_.Count == MIDDAYS);

                double dea = DiffPrices_.Average(); // DIFF 均线

                double macd = diff - dea; // DIFF与均线之差

                _DateIndicators.Add(startDate, macd);

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        public override OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (double.IsNaN(GetIndicatorValue(dt)) || double.IsNaN(GetIndicatorValue(prev)))
            {
                return OperType.NoOper;
            }

            if ((this.GetIndicatorValue(dt) > 0) && (this.GetIndicatorValue(prev) < 0))
            {
                return OperType.Buy;
            }
            else if ((this.GetIndicatorValue(dt) < 0) && (this.GetIndicatorValue(prev) > 0))
            {
                return OperType.Sell;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        private void AddTodayPrice(double price)
        {
            if (price < 0.01)
            {
                throw new ArgumentOutOfRangeException("MacdCalculator.AddTodayPrice: " + price.ToString());
            }

            LongPrices_.Add(price);
            if (LongPrices_.Count > LONGDAYS)
            {
                LongPrices_.RemoveAt(0);
            }

            ShortPrices_.Add(price);
            if (ShortPrices_.Count > SHORTDAYS)
            {
                ShortPrices_.RemoveAt(0);
            }
        }

        private bool IsCountEnough()
        {
            return (LongPrices_.Count >= LONGDAYS) && (ShortPrices_.Count >= SHORTDAYS);
        }

        List<double> LongPrices_ = new List<double>();
        List<double> ShortPrices_ = new List<double>();
        List<double> DiffPrices_ = new List<double>();

        private const int SHORTDAYS = 12;
        private const int LONGDAYS = 26;
        private const int MIDDAYS = 9; // 计算DIFF的平均线 
    }
}
