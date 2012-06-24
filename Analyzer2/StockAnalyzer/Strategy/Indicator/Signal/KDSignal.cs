using System;
using Stock.Common.Data;
using System.Collections.Generic;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class KDSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }
            MaxPrices_.AddLast(sd.MaxPrice);
            MinPrices_.AddLast(sd.MinPrice);

            if (MaxPrices_.Count <= KD_CALC_DAYS)
            {
                return false;
            }

            MaxPrices_.RemoveFirst();
            MinPrices_.RemoveFirst();

            CalculateKD(sd.EndPrice);

            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "KDJ Signal";
        }

        #endregion

        private void CalculateKD(double endPrice)
        {
            List<double> maxPriceList = new List<double>();
            List<double> minPriceList = new List<double>();
            maxPriceList.AddRange(MaxPrices_);
            minPriceList.AddRange(MinPrices_);

            minPriceList.Sort();
            maxPriceList.Sort();

            double minPriceOfNDays = minPriceList[0];
            double maxPriceOfNDays = maxPriceList[maxPriceList.Count - 1]; // 最后一个
            double rsv = (endPrice - minPriceOfNDays) * 100 / (maxPriceOfNDays - minPriceOfNDays);

            CurrentK_ = (1.0 / 3) * rsv + (2.0 / 3) * PreviousK_;
            CurrentD_ = (1.0 / 3) * CurrentK_ + (2.0 / 3) * PreviousD_;

            // 低位K上穿D线
            if (KUpCrossD(CurrentK_, CurrentD_, PreviousK_, PreviousD_))
            {
                TodayOper_ = OperType.Buy;
            }
            // 高位K下穿D线
            else if (KDownCrossD(CurrentK_, CurrentD_, PreviousK_, PreviousD_))
            {
                TodayOper_ = OperType.Sell;
            }
            else
            {
                TodayOper_ = OperType.NoOper;
            }

            PreviousK_ = CurrentK_;
            PreviousD_ = CurrentD_;
        }

        private bool KUpCrossD(double k, double d, double prevK, double prevD)
        {
            return (k < KdMinMargin_) && (d < KdMinMargin_) && (k > d) && (prevK < prevD);
        }

        private bool KDownCrossD(double k, double d, double prevK, double prevD)
        {
            return (k > KdMaxMargin_) && (d > KdMaxMargin_) && (k < d) && (prevK > prevD);
        }

        /// <summary>
        /// KD Calculator
        /// </summary>
        /// <param name="kdmin">0~100, Normally 0~30</param>
        /// <param name="kdmax">0~100, Normally 70~100</param>
        public KDSignal(double kdmin, double kdmax)
        {
            KdMinMargin_ = kdmin;
            KdMaxMargin_ = kdmax;

            PreviousK_ = DEFAULT_KD_VALUE;
            PreviousD_ = DEFAULT_KD_VALUE;

            CurrentK_ = DEFAULT_KD_VALUE;
            CurrentD_ = DEFAULT_KD_VALUE;
        }

        double KdMinMargin_; // 低位门限
        double KdMaxMargin_; // 高位门限

        private LinkedList<double> MaxPrices_ = new LinkedList<double>();
        private LinkedList<double> MinPrices_ = new LinkedList<double>();

        double PreviousK_;
        double PreviousD_;
        double CurrentK_;
        double CurrentD_;

        OperType TodayOper_;

        private const double DEFAULT_KD_VALUE = 50;

        private const int KD_CALC_DAYS = 9;
    }
}
