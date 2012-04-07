﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.KDJ;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    /// <summary>
    /// KD based calculator
    /// </summary>
    class KdCalc : BasicIndicatorCalc
    {
        /// <summary>
        /// KD Calculator
        /// </summary>
        /// <param name="kdmin">0~100, Normally 0~30</param>
        /// <param name="kdmax">0~100, Normally 70~100</param>
        public KdCalc(double kdmin, double kdmax)
        {
            KdMinMargin_ = kdmin;
            KdMaxMargin_ = kdmax;
        }

        public override string Name
        {
            get { return "KD2 " + KdMinMargin_.ToString() + ", " + KdMaxMargin_.ToString(); }
        }

        public override void Calc(IStockHistory hist)
        {
            IKdjCalculator kdjCalc = new KdjCalculator();

            kdjCalc.Calc(hist);

            IKdjStorage kdStorage = kdjCalc.GetStorage();

            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                double k = kdStorage.GetK(startDate);
                double d = kdStorage.GetD(startDate);

                DateTime yesterday = DateFunc.GetPreviousWorkday(startDate);
                double prevK = kdStorage.GetK(yesterday);
                double prevD = kdStorage.GetD(yesterday);

                // 低位K上穿D线
                if (KUpCrossD(k, d, prevK, prevD))
                {
                    DateToOpers_.Add(startDate, OperType.Buy);
                }

                // 高位K下穿D线
                if (KDownCrossD(k, d, prevK, prevD))
                {
                    DateToOpers_.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        private bool KUpCrossD(double k, double d, double prevK, double prevD)
        {
            return (k < KdMinMargin_) && (d < KdMinMargin_) && (k > d) && (prevK < prevD);
        }

        private bool KDownCrossD(double k, double d, double prevK, double prevD)
        {
            return (k > KdMaxMargin_) && (d > KdMaxMargin_) && (k < d) && (prevK > prevD);
        }

        double KdMinMargin_; // 低位门限
        double KdMaxMargin_; // 高位门限
    }
}
