using System;
using System.Globalization;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Business.Shape;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
{
    class SpikeVolumeShapeCalc : BasicIndicatorCalc
    {
        public SpikeVolumeShapeCalc(double deltaratio)
        {
            _DeltaRatio = deltaratio;
        }

        public override string Name
        {
            get { return "SpikeVolumeShape Old " + _DeltaRatio.ToString("F03", CultureInfo.CurrentCulture); }
        }

        public override void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                IStockData prevData = hist.GetPrevDayStock(startDate);

                if ((stock == null) || (prevData == null))
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                if (ShapeJudger.IsT2(stock, _DeltaRatio) && VolumeHelper.IsLargerThan(stock, prevData, 0.3))
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
