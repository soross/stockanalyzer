using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class HoldSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }

            if (PreviousStock_ == null)
            {
                PreviousStock_ = sd;
                FirstDaySetted = true;
            }

            return true;
        }

        public OperType GetSignal()
        {
            if (FirstDaySetted)
            {
                FirstDaySetted = false;
                return OperType.Buy;                
            }
            else
            {
                return OperType.NoOper;
            }
        }

        public string GetName()
        {
            return "Hold Signal";
        }

        #endregion

        public static bool IsValidPrice(double price)
        {
            return price > 0;
        }

        IStockData PreviousStock_ = null;
        bool FirstDaySetted = false;
    }
}
