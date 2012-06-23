using System;
using Stock.Common.Data;
using System.Collections.Generic;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class KDSignal : ISignalCalculator
    {
        public KDSignal()
        {
            prevK = DEFAULT_KD_VALUE;
            prevD = DEFAULT_KD_VALUE;

            CurrentK = DEFAULT_KD_VALUE;
            CurrentD = DEFAULT_KD_VALUE;
        }

        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }
            MaxPrices_.AddLast(sd.MaxPrice);
            MinPrices_.AddLast(sd.MinPrice);

            if (MaxPrices_.Count < KD_CALC_DAYS)
            {
                return false;
            }
            return false;
        }

        public OperType GetSignal()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "KDJ";
        }

        #endregion

        /// <summary>
        /// KD Calculator
        /// </summary>
        /// <param name="kdmin">0~100, Normally 0~30</param>
        /// <param name="kdmax">0~100, Normally 70~100</param>
        public KDSignal(double kdmin, double kdmax)
        {
            KdMinMargin_ = kdmin;
            KdMaxMargin_ = kdmax;
        }

        double KdMinMargin_; // 低位门限
        double KdMaxMargin_; // 高位门限

        private LinkedList<double> MaxPrices_ = new LinkedList<double>();
        private LinkedList<double> MinPrices_ = new LinkedList<double>();

        double prevK;
        double prevD;
        double CurrentK;
        double CurrentD;

        private const double DEFAULT_KD_VALUE = 50;

        private const int KD_CALC_DAYS = 9;
    }
}
