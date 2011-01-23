using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            if (curShortAvg > curLongAvg)
            {
                double val = (curLongAvg - curShortAvg) * LongDays * ShortDays / (LongDays - ShortDays);
                return (val + 0.01);
            }
            else
            {
                double val = (curLongAvg - curShortAvg) * LongDays * ShortDays / (LongDays - ShortDays);
                return (val - 0.01);
            }
        }

        public void AddPrice(double price)
        {
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
