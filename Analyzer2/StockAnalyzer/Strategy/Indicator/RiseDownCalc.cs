using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Strategy.TradeRule;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class RiseDownCalc : BasicIndicatorCalc
    {
        public RiseDownCalc(double percent)
        {
            _Rule = new RiseDownRule(percent);
        }

        public override string Name
        {
            get { return "RiseDown"; }
        }

        // 指令为比最高点下降的比例，比最低点上升的比例
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

                DateTime prevDate = DateFunc.GetPreviousWorkday(startDate);
                IStockData prevStock = hist.GetStock(prevDate);

                if (prevStock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                OperType operType = _Rule.Execute(stock.EndPrice, prevStock.EndPrice);
                if (operType != OperType.NoOper)
                {
                    _DateToOpers.Add(startDate, operType);
                }                            

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        RiseDownRule _Rule;
    }
}
