using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.Rise;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class ThreeDayCalc : BasicIndicatorCalc
    {
        public override string Name
        {
            get { return "3Days: " + _Judger.Name; }
        }

        public override void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData currentstock = hist.GetStock(startDate);

                DateTime prevDate = DateFunc.GetPreviousWorkday(startDate);
                IStockData prevStock = hist.GetStock(prevDate);

                DateTime prev2Date = DateFunc.GetPreviousWorkday(prevDate);
                IStockData prev2Stock = hist.GetStock(prev2Date);

                if ((currentstock == null) || (prevStock == null) || (prev2Stock == null))
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                if (_Judger.FulFil(prev2Stock, prevStock, currentstock))
                {
                    _DateToOpers.Add(startDate, OperType.Sell);
                }
                else if (_Judger.ReverseFulFil(prev2Stock, prevStock, currentstock))
                {
                    _DateToOpers.Add(startDate, OperType.Buy);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }


        public ThreeDayCalc(IStockJudger judger)
        {
            _Judger = judger;
        }

        private IStockJudger _Judger;
    }
}
