using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Business.Shape;
using System.Globalization;
using FinanceAnalyzer.Business;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class SpikeVolumeShapeSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }
            prevStock_ = currentStock_;
            currentStock_ = sd;

            if ((prevStock_ == null) || (currentStock_ == null))
            {
                return false;
            }

            if (ShapeJudger.IsT2(currentStock_, DeltaRatio_) && VolumeHelper.IsLargerThan(currentStock_, prevStock_, 0.3))
            {
                TodayOper_ = OperType.Buy;
            }
            else if (ShapeJudger.IsReverseT2(currentStock_, DeltaRatio_))
            {
                TodayOper_ = OperType.Sell;
            }
            else
            {
                TodayOper_ = OperType.NoOper;
            }

            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "SpikeVolumeShape " + DeltaRatio_.ToString("F03", CultureInfo.CurrentCulture); ;
        }

        #endregion

        public SpikeVolumeShapeSignal(double deltaratio)
        {
            DeltaRatio_ = deltaratio;
        }

        IStockData prevStock_ = null;
        IStockData currentStock_ = null;

        OperType TodayOper_;
        double DeltaRatio_;
    }
}
