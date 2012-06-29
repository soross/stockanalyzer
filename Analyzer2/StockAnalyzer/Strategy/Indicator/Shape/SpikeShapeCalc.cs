using System;
using System.Globalization;
using FinanceAnalyzer.Business.Shape;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
{
    /// <summary>
    /// Spike low means buy, spike high means sell
    /// </summary>
    class SpikeShapeCalc : BasicIndicatorCalc
    {
        public SpikeShapeCalc(double deltaratio)
        {
            _DeltaRatio = deltaratio;
        }

        public override string Name
        {
            get { return "SpikeShape Old " + _DeltaRatio.ToString("F03", CultureInfo.CurrentCulture); }
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

                if (ShapeJudger.IsT2(stock, _DeltaRatio))
                {
                    DateToOpers_.Add(startDate, OperType.Buy);
                }
                else if (ShapeJudger.IsReverseT2(stock, _DeltaRatio))
                {
                    DateToOpers_.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        double _DeltaRatio;
    }
}
