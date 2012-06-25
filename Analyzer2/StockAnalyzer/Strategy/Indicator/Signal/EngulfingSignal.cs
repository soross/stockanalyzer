using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    /// <summary>
    /// See: http://www.stockta.com/cgi-bin/school.pl
    /// </summary>
    class EngulfingSignal : ISignalCalculator
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

            EngulfingType et = JudgeEngulfing(prevStock_, currentStock_);

            if (et == EngulfingType.RED_ENGULFING)
            {
                TodayOper_ = OperType.Buy;
            }
            else if (et == EngulfingType.GREEN_ENGULFING)
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
            return TodayOper_; ;
        }

        public string GetName()
        {
            return "Engulfing";
        }

        #endregion

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

        IStockData prevStock_ = null;
        IStockData currentStock_ = null;

        OperType TodayOper_;
    }
}
