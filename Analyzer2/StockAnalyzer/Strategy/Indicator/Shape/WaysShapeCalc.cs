using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Business.Shape;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator.Shape
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

                IStockData prevData = hist.GetPrevDayStock(startDate);

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
