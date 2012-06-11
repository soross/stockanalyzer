using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class KDSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public void AddStock(IStockData sd)
        {
            throw new NotImplementedException();
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
    }
}
