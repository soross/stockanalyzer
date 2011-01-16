using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class SimpleShapeCalc : BasicIndicatorCalc
    {
        public override string Name
        {
            get { return "SimpleShape"; }
        }

        public override void Calc(IStockHistory hist)
        {
            const int CURVEDAYS = 5;
            SlopeCalculator slopeCalc = new SlopeCalculator(CURVEDAYS);

            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                if (stock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                slopeCalc.AddVal(stock.EndPrice);
                if (!slopeCalc.IsEnough())
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                if (slopeCalc.IsDownPeriod() && ShapeJudger.IsUpCross(stock))
                {
                    _DateToOpers.Add(startDate, OperType.Buy);
                }
                else if (slopeCalc.IsRisePeriod() && ShapeJudger.IsDownCross(stock))
                {
                    _DateToOpers.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }
    }
}
