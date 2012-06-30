using FinanceAnalyzer.Business.Shape;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class WaysShapeSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }
            prevStock_ = currentStock_;
            currentStock_ = sd;

            if ((prevStock_ == null) || (currentStock_ == null))
            {
                return false;
            }

            TodayOper_ = Scanner_.Analyse(currentStock_, prevStock_);
            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "WaysShape";
        }

        #endregion

        public WaysShapeSignal(IShapeScanner scanner)
        {
            Scanner_ = scanner;
        }

        IShapeScanner Scanner_;

        IStockData prevStock_ = null;
        IStockData currentStock_ = null;

        OperType TodayOper_;
    }
}
