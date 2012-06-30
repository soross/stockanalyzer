using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business.Shape
{
    /// <summary>
    /// Stock prices shape
    /// </summary>
    public class ShapeJudger
    {
        /// <summary>
        /// Init using Stock data of one day
        /// </summary>
        /// <param name="data">Stock data of one day</param>
        public ShapeJudger(IStockData data)
        {
            StockData_ = data;
        }

        public bool IsReverseT()
        {
            return IsReverseT(StockData_);
        }

        public static bool IsCross(IStockData data)
        {
            double deltapercent = StockDataCalc.GetRisePercent(data);

            double riseRatio = StockDataCalc.GetRisePercent(data.StartPrice, data.MaxPrice);
            double downRatio = StockDataCalc.GetRisePercent(data.StartPrice, data.MinPrice);
            double swingRatio = riseRatio      + downRatio;
            return ((Math.Abs(deltapercent) < CROSSDELTAPERCENT)
                && (data.MaxPrice > data.StartPrice)
                && (data.MinPrice < data.EndPrice)
                && (swingRatio < CROSSDELTAPERCENT)
                && (riseRatio > CROSSSWINGPERCENT));
        }

        public static bool IsUpCross(IStockData data)
        {
            double deltapercent = StockDataCalc.GetRisePercent(data);
            bool isCross = ((Math.Abs(deltapercent) < CROSSDELTAPERCENT)
                && (data.MaxPrice > data.StartPrice)
                && (data.MinPrice < data.EndPrice));

            return isCross && (data.EndPrice > data.StartPrice);
        }

        public static bool IsDownCross(IStockData data)
        {
            double deltapercent = StockDataCalc.GetRisePercent(data);
            bool isCross = ((Math.Abs(deltapercent) < CROSSDELTAPERCENT)
                && (data.MaxPrice > data.StartPrice)
                && (data.MinPrice < data.EndPrice));

            return isCross && (data.EndPrice < data.StartPrice);
        }

        public static bool IsT(IStockData data)
        {
            return IsT2(data, 0.03);
        }

        public static bool IsT2(IStockData data, double ratio)
        {
            double deltaEndMin = (data.EndPrice - data.MinPrice) / data.MinPrice;
            double deltaStartMax = (data.MaxPrice - data.StartPrice) / data.StartPrice;
            double deltaEndMax = (data.MaxPrice - data.EndPrice) / data.EndPrice;

            return ((deltaEndMin >= ratio)
                && (deltaStartMax <= (ratio / 4)) && (deltaEndMax <= (ratio / 4)));
        }

        public static bool IsReverseT(IStockData data)
        {
            return IsReverseT2(data, 0.03);
        }

        public static bool IsReverseT2(IStockData data, double ratio)
        {
            double deltaEndMax = (data.MaxPrice - data.EndPrice) / data.EndPrice;
            double deltaStartMin = (data.StartPrice - data.MinPrice) / data.MinPrice;
            double deltaEndMin = (data.EndPrice - data.MinPrice) / data.MinPrice;

            return ((deltaEndMax >= ratio)
                && (deltaStartMin <= ratio / 4) && (deltaEndMin <= ratio / 4));
        }

        IStockData StockData_;
        const double CROSSDELTAPERCENT = 0.01;
        const double CROSSSWINGPERCENT = 0.02;
    }
}
