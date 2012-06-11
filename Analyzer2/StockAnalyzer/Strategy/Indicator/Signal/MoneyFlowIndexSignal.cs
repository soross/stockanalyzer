using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class MoneyFlowIndexSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public void AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return;
            }

            prevStock = currentStock;
            currentStock = sd;

            throw new NotImplementedException();
        }

        public OperType GetSignal()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        #endregion

        private static double CalcTypicalPrice(IStockData val)
        {
            return (val.MaxPrice + val.MinPrice + val.EndPrice) / 3;
        }

        IStockData prevStock = null;
        IStockData currentStock = null;
    }
}
