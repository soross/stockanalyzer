using System;
using Stock.Common.Data;
using FinanceAnalyzer.Strategy.TradeRule;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class RiseDownSignal : ISignalCalculator
    {
        public RiseDownSignal(double percent)
        {
            Rule_ = new RiseDownRule(percent);
        }

        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }

            prevStock_ = currentStock_;
            currentStock_ = sd;

            if (prevStock_ == null)
            {
                return false;
            }

            TodayOper_ = Rule_.Execute(currentStock_.EndPrice, prevStock_.EndPrice);
            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "Rise Down";
        }

        #endregion

        IStockData prevStock_ = null;
        IStockData currentStock_ = null;

        RiseDownRule Rule_;
        OperType TodayOper_;
    }
}
