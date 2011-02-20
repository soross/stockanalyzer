using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business.Shape;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
{
    // Spike low means buy, spike high means sell
    class SpikeShapeCalc : BasicIndicatorCalc
    {
        public override string Name
        {
            get { return "SpikeShape"; }
        }

        public override void Calc(IStockHistory hist)
        {
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

                if (ShapeJudger.IsT2(stock))
                {
                    _DateToOpers.Add(startDate, OperType.Buy);
                }
                else if (ShapeJudger.IsReverseT2(stock))
                {
                    _DateToOpers.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }
    }
}
