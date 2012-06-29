using System;
using Stock.Common.Data;
using System.Globalization;
using FinanceAnalyzer.Business.Shape;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class SpikeShapeSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }
            if (ShapeJudger.IsT2(sd, DeltaRatio_))
            {
                TodayOper_ = OperType.Buy;
            }
            else if (ShapeJudger.IsReverseT2(sd, DeltaRatio_))
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
            return "SpikeShape " + DeltaRatio_.ToString("F03", CultureInfo.CurrentCulture); ;
        }

        #endregion

        public SpikeShapeSignal(double deltaratio)
        {
            DeltaRatio_ = deltaratio;
        }

        OperType TodayOper_;
        double DeltaRatio_;
    }
}
