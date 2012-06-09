using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    abstract class BasicSignalCalc : IIndicatorCalc
    {
        public BasicSignalCalc(ISignalCalculator signalCalc)
        {
            signalCalc_ = signalCalc;
        }

        public abstract string Name
        {
            get;
        }

        public void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                if (stock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                signalCalc_.AddStock(stock);

                OperType ot = signalCalc_.GetSignal();
                if (ot != OperType.NoOper)
                {
                    DateToOpers_.Add(startDate, ot);
                }
 
                startDate = DateFunc.GetNextWorkday(startDate);
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
