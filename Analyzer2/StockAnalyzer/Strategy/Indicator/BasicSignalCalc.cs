using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class BasicSignalCalc : IIndicatorCalc
    {
        public BasicSignalCalc(ISignalCalculator signalCalc)
        {
            signalCalc_ = signalCalc;
        }

        public string Name
        {
            get
            {
                return signalCalc_.GetName();
            }
        }

        public void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                CalculateSignal(startDate, stock);
 
                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        void CalculateSignal(DateTime dt, IStockData stock)
        {
            if (stock == null)
            {
                return;
            }

            if (!signalCalc_.AddStock(stock))
            {
                return;
            }

            OperType ot = signalCalc_.GetSignal();
            if (ot != OperType.NoOper)
            {
                DateToOpers_.Add(dt, ot);
            }
        }

        /// <summary>
        /// Return the buy or sell signal based on the indicator value
        /// </summary>
        /// <param name="dt">Current date</param>
        /// <param name="prev">Previous date</param>
        /// <returns>buy or sell signal</returns>
        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (DateToOpers_.ContainsKey(dt))
            {
                return DateToOpers_[dt];
            }

            return OperType.NoOper;
        }

        private Dictionary<DateTime, OperType> DateToOpers_ = new Dictionary<DateTime, OperType>();

        ISignalCalculator signalCalc_;
    }
}
