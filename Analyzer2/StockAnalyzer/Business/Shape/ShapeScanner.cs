using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Business.Shape
{
    class ShapeScanner : IShapeScanner
    {
        public OperType Analyse(IStockData stock, IStockData prevStock)
        {
            if ((stock == null) || (prevStock == null))
            {
                return OperType.NoOper;
            }

            double deltapercent = StockDataCalc.GetRisePercent(stock);

            double prevPercent = StockDataCalc.GetRisePercent(prevStock);

            // Only for converse
            if (NumbericHelper.IsSameSign(deltapercent, prevPercent))
            {
                return OperType.NoOper;
            }

            // ratio should be larger than yesterday
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
