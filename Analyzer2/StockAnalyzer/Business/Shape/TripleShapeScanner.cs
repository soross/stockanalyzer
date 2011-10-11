using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Business.Shape
{
    class TripleShapeScanner : ITripleShapeScanner
    {
        public OperType Analyse(IStockData prevStock, IStockData stock, IStockData nextStock)
        {
            if ((stock == null) || (prevStock == null) || (nextStock == null))
            {
                return OperType.NoOper;
            }

            double deltapercent = StockDataCalc.GetRisePercent(stock);
            //double prevPercent = StockDataCalc.GetRisePercent(prevStock);
            double nextPercent = StockDataCalc.GetRisePercent(nextStock);

            if (NumbericHelper.IsSameSign(deltapercent, nextPercent))
            {
                return OperType.NoOper;
            }

            if ((nextPercent > 0.03) && (nextStock.EndPrice > prevStock.StartPrice)
                && (nextStock.EndPrice > stock.StartPrice))
            {
                return OperType.Buy;
            }

            if ((nextPercent < -0.03) && (nextStock.EndPrice < prevStock.StartPrice)
                 && (nextStock.EndPrice < stock.StartPrice))
            {
                return OperType.Sell;
            }

            return OperType.NoOper;
        }        
    }    
}
