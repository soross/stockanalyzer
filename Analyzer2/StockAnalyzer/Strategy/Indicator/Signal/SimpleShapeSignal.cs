using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Business.Shape;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class SimpleShapeSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }

            slopeCalc.AddVal(sd.EndPrice);
            if (!slopeCalc.IsEnough())
            {
                return false;
            }

            if (slopeCalc.IsDownPeriod() && ShapeJudger.IsUpCross(sd))
            {
                TodayOper_ = OperType.Buy;
            }
            else if (slopeCalc.IsRisePeriod() && ShapeJudger.IsDownCross(sd))
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
            return "SimpleShape";
        }

        #endregion

        private const int CURVEDAYS = 5;
        private SlopeCalculator slopeCalc = new SlopeCalculator(CURVEDAYS);

        OperType TodayOper_;
    }
}
