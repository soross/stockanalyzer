using FinanceAnalyzer.Strategy.Judger;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    /// <summary>
    /// Same up or down direction within continuous three days trigger the sell or buy signal
    /// </summary>
    class ThreeDaySignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }

            stocks_.Add(sd);

            if (!stocks_.IsEnough())
            {
                return false;
            }

            if (Judger_.FulFil(stocks_.GetAt(0), stocks_.GetAt(1), stocks_.GetAt(2)))
            {
                TodayOper_ = OperType.Sell;
            }
            else if (Judger_.ReverseFulFil(stocks_.GetAt(0), stocks_.GetAt(1), stocks_.GetAt(2)))
            {
                TodayOper_ = OperType.Buy;
            }
            else
            {
                TodayOper_ = OperType.NoOper;
            }

            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "3 Day Signal";
        }

        #endregion

        public ThreeDaySignal(IStockJudger judger)
        {
            Judger_ = judger;
        }

        private IStockJudger Judger_;
        OperType TodayOper_;
        FixedSizeContainer<IStockData> stocks_ = new FixedSizeContainer<IStockData>(3);
    }
}
