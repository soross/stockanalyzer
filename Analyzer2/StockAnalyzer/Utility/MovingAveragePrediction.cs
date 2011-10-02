using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FinanceAnalyzer.Utility
{
    class MovingAveragePrediction
    {
        public int LongDays
        {
            get;
            set;
        }

        public int ShortDays
        {
            get;
            set;
        }

        public double CalcNextPredictionValue()
        {
            if (!IsCountEnough())
            {
                return double.NaN;
            }

            double curLongAvg = _LongPriceList.Average();
            double curShortAvg = _ShortPriceList.Average();

            double val1 = (curLongAvg - _LongPriceList.First()) * ShortDays;
            double val2 = (curShortAvg - _ShortPriceList.First()) * LongDays;

            double val = (val1 - val2) / (LongDays - ShortDays);
            return val;
        }

        public void AddPrice(double price)
        {
            if (price < 0.01)
            {
                throw new ArgumentOutOfRangeException("MovingAveragePrediction.AddPrice: " 
                    + price.ToString(CultureInfo.CurrentCulture));
            }

            _LongPriceList.Add(price);
            if (_LongPriceList.Count > LongDays)
            {
                _LongPriceList.RemoveAt(0);
            }

            _ShortPriceList.Add(price);
            if (_ShortPriceList.Count > ShortDays)
            {
                _ShortPriceList.RemoveAt(0);
            }
        }

        public bool IsCountEnough()
        {
            return (_LongPriceList.Count >= LongDays) && (_ShortPriceList.Count >= ShortDays);
        }

        public double GetLongAverage()
        {
            return _LongPriceList.Average();
        }

        public double GetShortAverage()
        {
            return _ShortPriceList.Average();
        }

        List<double> _LongPriceList = new List<double>();
        List<double> _ShortPriceList = new List<double>();
    }
}
