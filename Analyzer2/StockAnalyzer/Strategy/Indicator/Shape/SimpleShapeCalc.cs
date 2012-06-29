using System;
using FinanceAnalyzer.Business.Shape;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
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
                    DateToOpers_.Add(startDate, OperType.Buy);
                }
                else if (slopeCalc.IsRisePeriod() && ShapeJudger.IsDownCross(stock))
                {
                    DateToOpers_.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }
    }
}
