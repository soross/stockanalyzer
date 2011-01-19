using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Business.Shape;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class WaysShapeCalc : BasicIndicatorCalc
    {
        public WaysShapeCalc(IShapeScanner scanner)
        {
            _Scanner = scanner;
        }

        public override string Name
        {
            get { return "WaysShape"; }
        }

        public override void Calc(IStockHistory hist)
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

                IStockData prevData = hist.GetPrevDayStock(startDate);
                if (prevData == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                OperType tp = _Scanner.Analyse(stock, prevData);
                if (tp != OperType.NoOper)
                {
                    _DateToOpers.Add(startDate, tp);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        IShapeScanner _Scanner;
    }
}
