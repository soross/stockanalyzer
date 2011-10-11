using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business.Shape
{
    class ShapeJudger
    {
        public ShapeJudger(IStockData data)
        {
            _StockData = data;
        }

        public bool IsReverseT()
        {
            return IsReverseT(_StockData);
        }

        public static bool IsCross(IStockData data)
        {
            double deltapercent = StockDataCalc.GetRisePercent(data);
            return ((Math.Abs(deltapercent) < CROSSDELTAPERCENT)
                && (data.MaxPrice > data.StartPrice)
                && (data.MinPrice < data.EndPrice));
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
            double deltapercent = (data.EndPrice - data.MinPrice) / data.MinPrice;
            double deltaMax = (data.MaxPrice - data.EndPrice) / data.EndPrice;

            return ((data.EndPrice >= data.StartPrice)
                && (deltapercent >= 0.02)
                && (deltaMax <= 0.005));
        }

        public static bool IsReverseT(IStockData data)
        {
            double deltapercent = (data.MaxPrice - data.EndPrice) / data.EndPrice;
            double deltaMin = (data.EndPrice - data.MinPrice) / data.MinPrice;

            return ((data.EndPrice <= data.StartPrice)
                && (deltapercent >= 0.02)
                && (deltaMin <= 0.005));
        }

        public static bool IsT2(IStockData data, double ratio)
        {
            double deltaEndMin = (data.EndPrice - data.MinPrice) / data.MinPrice;
            double deltaStartMin = (data.StartPrice - data.MinPrice) / data.MinPrice;
            double deltaEndMax = (data.MaxPrice - data.EndPrice) / data.MaxPrice;

            return ((deltaEndMin >= ratio)
                && (deltaStartMin >= ratio) && (deltaEndMax <= ratio - 0.005));
        }

        public static bool IsReverseT2(IStockData data, double ratio)
        {
            double deltaEndMax = (data.MaxPrice - data.EndPrice) / data.MaxPrice;
            double deltaStartMax = (data.MaxPrice - data.StartPrice) / data.MaxPrice;
            double deltaEndMin = (data.EndPrice - data.MinPrice) / data.MinPrice;

            return ((deltaEndMax >= ratio)
                && (deltaStartMax >= ratio) && (deltaEndMin <= ratio - 0.005));
        }

        IStockData _StockData;
        const double CROSSDELTAPERCENT = 0.01;
    }
}
