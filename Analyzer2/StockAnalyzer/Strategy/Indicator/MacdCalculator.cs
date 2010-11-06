using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using System.Linq;
using System.Diagnostics;
using FinanceAnalyzer.Strategy.Indicator;

namespace FinanceAnalyzer.Strategy.Indicator
{
    // 参考：http://en.wikipedia.org/wiki/MACD
    // 传统MACD计算方式 
    class MacdCalculator : IIndicatorCalc
    {
        public string Name
        {
            get { return "MACD"; }
        }

        public void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                StockData stock = hist.GetStock(startDate);
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

                Debug.Assert(_LongPriceList.Count == LONGDAYS);
                Debug.Assert(_ShortPriceList.Count == SHORTDAYS);

                // 计算长周期和短周期均值
                double longEMA = _LongPriceList.Average();
                double shortEMA = _ShortPriceList.Average();

                double diff = shortEMA - longEMA; // 短周期均值-长周期均值

                _DiffPriceList.Add(diff);
                if (_DiffPriceList.Count < MIDDAYS)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }
                else if (_DiffPriceList.Count > MIDDAYS)
                {
                    _DiffPriceList.RemoveAt(0);
                }
                Debug.Assert(_DiffPriceList.Count == MIDDAYS);

                double dea = _DiffPriceList.Average(); // DIFF 均线

                double macd = diff - dea; // DIFF与均线之差

                _DateMacd.Add(startDate, macd);

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        // 得到某天的MACD指标值
        public double GetIndicatorValue(DateTime dt)
        {
            if (_DateMacd.ContainsKey(dt))
            {
                return _DateMacd[dt];
            }
            else
            {
                return 0.0; // 默认返回0 
            }
        }

        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
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
            _LongPriceList.Add(price);
            if (_LongPriceList.Count > LONGDAYS)
            {
                _LongPriceList.RemoveAt(0);
            }

            _ShortPriceList.Add(price);
            if (_ShortPriceList.Count > SHORTDAYS)
            {
                _ShortPriceList.RemoveAt(0);
            }
        }

        private bool IsCountEnough()
        {
            return (_LongPriceList.Count >= LONGDAYS) && (_ShortPriceList.Count >= SHORTDAYS);
        }

        List<double> _LongPriceList = new List<double>();
        List<double> _ShortPriceList = new List<double>();
        List<double> _DiffPriceList = new List<double>();

        private Dictionary<DateTime, double> _DateMacd = new Dictionary<DateTime, double>();

        private const int SHORTDAYS = 12;
        private const int LONGDAYS = 26;
        private const int MIDDAYS = 9; // 计算DIFF的平均线 
    }
}
