using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Business.Shape;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
{
    class TripleShapeCalc : BasicIndicatorCalc
    {
        public TripleShapeCalc(ITripleShapeScanner scanner)
        {
            _Scanner = scanner;
        }

        public override string Name
        {
            get { return "TripleShape"; }
        }

        public override void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                IStockData prevData = hist.GetPrevDayStock(startDate);
                IStockData prev2Data = hist.GetPrevDayStock(hist.GetPreviousDay(startDate));

                OperType tp = _Scanner.Analyse(prev2Data, prevData, stock);
                if (tp != OperType.NoOper)
                {
                    _DateToOpers.Add(startDate, tp);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        ITripleShapeScanner _Scanner;
    }
}
