using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class KDJAlgorithm
    {
        public void AddValue(double maxVal, double minVal)
        {
            MaxPrices_.AddLast(maxVal);
            MinPrices_.AddLast(minVal);

            if (TotalCount_ <= KD_CALC_DAYS)
            {
            }
            else
            {
            }

            TotalCount_++;
        }

        int TotalCount_ = 0;

        private LinkedList<double> MaxPrices_ = new LinkedList<double>();
        private LinkedList<double> MinPrices_ = new LinkedList<double>();

        private const double DEFAULT_KD_VALUE = 50;

        private const int KD_CALC_DAYS = 9;
    }
}
