using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Utility
{
    class ShapeJudger
    {
        public ShapeJudger(StockData data)
        {
            _StockData = data;
        }

        public bool IsCross()
        {
            return IsCross(_StockData);
        }

        public bool IsT()
        {
            return IsT(_StockData);
        }

        public bool IsReverseT()
        {
            return IsReverseT(_StockData);
        }

        public static bool IsCross(StockData data)
        {
            double deltapercent = (data.EndPrice - data.StartPrice) / data.StartPrice;
            return ((Math.Abs(deltapercent) < CROSSDELTAPERCENT)
                && (data.MaxPrice > data.StartPrice)
                && (data.MinPrice < data.EndPrice));
        }

        public static bool IsUpCross(StockData data)
        {
            double deltapercent = (data.EndPrice - data.StartPrice) / data.StartPrice;
            bool isCross = ((Math.Abs(deltapercent) < CROSSDELTAPERCENT)
                && (data.MaxPrice > data.StartPrice)
                && (data.MinPrice < data.EndPrice));

            return isCross && (data.EndPrice > data.StartPrice);
        }

        public static bool IsDownCross(StockData data)
        {
            double deltapercent = (data.EndPrice - data.StartPrice) / data.StartPrice;
            bool isCross = ((Math.Abs(deltapercent) < CROSSDELTAPERCENT)
                && (data.MaxPrice > data.StartPrice)
                && (data.MinPrice < data.EndPrice));

            return isCross && (data.EndPrice < data.StartPrice);
        }

        public static bool IsT(StockData data)
        {
            double deltapercent = (data.EndPrice - data.MinPrice) / data.MinPrice;
            double deltaMax = (data.MaxPrice - data.EndPrice) / data.EndPrice;

            return ((data.EndPrice >= data.StartPrice)
                && (deltapercent >= 0.02)
                && (deltaMax <= 0.005));
        }

        public static bool IsReverseT(StockData data)
        {
            double deltapercent = (data.MaxPrice - data.EndPrice) / data.EndPrice;
            double deltaMin = (data.EndPrice - data.MinPrice) / data.MinPrice;

            return ((data.EndPrice <= data.StartPrice)
                && (deltapercent >= 0.02)
                && (deltaMin <= 0.005));
        }

        StockData _StockData;
        const double CROSSDELTAPERCENT = 0.01;
    }
}
