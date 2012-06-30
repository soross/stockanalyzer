using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Business.Shape;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class TripleShapeSignal : ISignalCalculator
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

            TodayOper_ = Scanner_.Analyse(stocks_.GetAt(0), stocks_.GetAt(1), stocks_.GetAt(2)); 
            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "TripleShape";
        }

        #endregion

        public TripleShapeSignal(ITripleShapeScanner scanner)
        {
            Scanner_ = scanner;
        }

        ITripleShapeScanner Scanner_;
        OperType TodayOper_;

        FixedSizeContainer<IStockData> stocks_ = new FixedSizeContainer<IStockData>(3);
    }
}
