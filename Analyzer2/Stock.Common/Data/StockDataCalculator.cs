using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Common.Data
{
    public class StockDataCalculator
    {
        public static double GetRiseRatio(IStockData sd)
        {
            return (sd.EndPrice - sd.StartPrice) / sd.StartPrice;
        }

        public static bool IsRise(IStockData sd)
        {
            return sd.EndPrice > sd.StartPrice;
        }

        public static bool IsDown(IStockData sd)
        {
            return sd.EndPrice < sd.StartPrice;
        }

        public static bool IsDifferentRiseDown(IStockData sd1, IStockData sd2)
        {
            if (IsRise(sd1) && IsDown(sd2))
            {
                return true;
            }
            else if (IsDown(sd1) && IsRise(sd2))
            {
                return true;
            }

            return false;
        }
    }
}
