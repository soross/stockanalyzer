using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class VolumeCalc : BasicIndicatorCalc
    {
        public VolumeCalc(double buyMargin, double sellMargin)
        {
            _BuyMargin = buyMargin;
            _SellMargin = sellMargin;
        }

        public override string Name
        {
            get { return "Volume: " + _BuyMargin.ToString() + ": " + _SellMargin.ToString(); }
        }

        public override void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData currentstock = hist.GetStock(startDate);

                if (currentstock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                if (!_Averager.IsEnough())
                {
                    _Averager.AddVal(currentstock.Amount);
                    continue;
                }

                ICollection<StockOper> opers = new List<StockOper>();
                double avgMinAmount = _Averager.GetValue() * (1 - _BuyMargin);
                double avgMaxAmount = _Averager.GetValue() * (1 + _SellMargin);

                _Averager.AddVal(currentstock.Amount);

                if (currentstock.Amount < avgMinAmount)
                {
                    // 成交地量，买入 
                    _DateToOpers.Add(startDate, OperType.Buy);
                }
                else if (currentstock.Amount > avgMaxAmount)
                {
                    // 成交天量，卖出
                    _DateToOpers.Add(startDate, OperType.Sell);
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        ValueAverager<double> _Averager = new ValueAverager<double>(AVERAGEDAYS);

        const int AVERAGEDAYS = 3;
        double _BuyMargin;
        double _SellMargin;
    }
}
