using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Utility
{
    class ShapeScanner
    {
        public static OperType Analyse(IStockData stock, IStockData prevStock)
        {
            double deltapercent = (stock.EndPrice - stock.StartPrice) / stock.StartPrice;

            double prevPercent = (prevStock.EndPrice - prevStock.StartPrice) / prevStock.StartPrice;

            if ((deltapercent * prevPercent) >= 0)
            {
                return OperType.NoOper;
            }

            if (Math.Abs(deltapercent) < Math.Abs(prevPercent))
            {
                return OperType.NoOper;
            }

            if ((deltapercent > 0.02) && (stock.EndPrice > prevStock.StartPrice))
            {
                return OperType.Buy;
            }

            if ((deltapercent < -0.02) && (stock.EndPrice < prevStock.StartPrice))
            {
                return OperType.Sell;
            }

            return OperType.NoOper;
        }
    }
}
