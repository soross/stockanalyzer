using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.KDJ;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class KdCalc : BasicIndicatorCalc
    {
        public KdCalc(double kdmin, double kdmax)
        {
            _KdMinMargin = kdmin;
            _KdMaxMargin = kdmax;
        }

        public override string Name
        {
            get { return "KD2 " + _KdMinMargin.ToString() + ", " + _KdMaxMargin.ToString(); }
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
                    _DateToOpers.Add(startDate, OperType.Buy);
                }

                // 高位K下穿D线
                if (KDownCrossD(k, d, prevK, prevD))
                {
                    _DateToOpers.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        private bool KUpCrossD(double k, double d, double prevK, double prevD)
        {
            return (k < _KdMinMargin) && (d < _KdMinMargin) && (k > d) && (prevK < prevD);
        }

        private bool KDownCrossD(double k, double d, double prevK, double prevD)
        {
            return (k > _KdMaxMargin) && (d > _KdMaxMargin) && (k < d) && (prevK > prevD);
        }

        double _KdMinMargin; // 低位门限
        double _KdMaxMargin; // 高位门限
    }
}
