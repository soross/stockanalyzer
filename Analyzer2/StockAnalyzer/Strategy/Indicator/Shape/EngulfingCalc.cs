using System;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
{
    /// <summary>
    /// See: http://www.stockta.com/cgi-bin/school.pl
    /// </summary>
    class EngulfingCalc : BasicIndicatorCalc
    {
        public override string Name
        {
            get { return "Engulfing Old"; }
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

                DateTime prevDate = DateFunc.GetPreviousWorkday(startDate);
                IStockData prevStock = hist.GetStock(prevDate);

                EngulfingType et = JudgeEngulfing(prevStock, stock);

                if (et == EngulfingType.RED_ENGULFING)
                {
                    DateToOpers_.Add(startDate, OperType.Buy);
                }
                else if (et == EngulfingType.GREEN_ENGULFING)
                {
                    DateToOpers_.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        enum EngulfingType
        {
            RED_ENGULFING,
            GREEN_ENGULFING,
            NOT_ENGULFING
        };

        static EngulfingType JudgeEngulfing(IStockData prevStock, IStockData todayStock)
        {
            if ((prevStock == null) || (todayStock == null))
            {
                return EngulfingType.NOT_ENGULFING;
            }

            if ((todayStock.MinPrice < prevStock.MinPrice)
                && (todayStock.MaxPrice > prevStock.MaxPrice))
            {
                if ((StockDataCalculator.GetRiseRatio(todayStock) > 0.01)
                    && StockDataCalculator.IsDifferentRiseDown(prevStock, todayStock))
                {
                    return EngulfingType.RED_ENGULFING;
                }
                else if ((StockDataCalculator.GetRiseRatio(todayStock) < -0.01)
                    && StockDataCalculator.IsDifferentRiseDown(prevStock, todayStock))
                {
                    return EngulfingType.GREEN_ENGULFING;
                }
                else
                {
                    return EngulfingType.NOT_ENGULFING;
                }
            }
            else
            {
                return EngulfingType.NOT_ENGULFING;
            }
        }
    }
}
