using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using System.Diagnostics;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class MoneyFlowIndexCalc : HistoricalValuesCalc
    {
        public override void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                IStockData prevStock = hist.GetPrevDayStock(startDate);
                if ((stock == null) || (prevStock == null))
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                double typicalPrice = GetTypicalPrice(stock);
                AddTodayPrice(typicalPrice * stock.Amount, StockJudger.IsRise(stock, prevStock)); // 直接保存Money Flow, 即Typical Price * Volume 

                if (_PriceList.Count < MFICALCDAYS)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                Debug.Assert(_PriceList.Count == MFICALCDAYS);

                double upSum = 0;
                double allSum = 0;
                for (int i = 0; i < _PriceList.Count; i++)
                {
                    allSum += _PriceList[i].Price;

                    if (_PriceList[i].IsUp)
                    {
                        upSum += _PriceList[i].Price;
                    }
                }

                double mfi = 100 * (upSum / allSum);

                _DateIndicators.Add(startDate, mfi); // 前 MFICALCDAYS 天数的MFI计算结果

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        public override OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (double.IsNaN(GetIndicatorValue(dt)))
            {
                return OperType.NoOper;
            }

            if (this.GetIndicatorValue(dt) < MFIBUYMARGIN)
            {
                return OperType.Buy;
            }
            else if (this.GetIndicatorValue(dt) > MFISELLMARGIN)
            {
                return OperType.Sell;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        public override string Name
        {
            get
            {
                return "MFI";
            }
        }

        private void AddTodayPrice(double price, bool isUp)
        {
            PriceProp prop = new PriceProp();

            prop.Price = price;
            prop.IsUp = isUp;

            _PriceList.Add(prop);
            if (_PriceList.Count > MFICALCDAYS)
            {
                _PriceList.RemoveAt(0);
            }
        }

        private static double GetTypicalPrice(IStockData val)
        {
            return (val.MaxPrice + val.MinPrice + val.EndPrice) / 3;
        }

        List<PriceProp> _PriceList = new List<PriceProp>();

        private const int MFICALCDAYS = 14; // MFI计算周期 
        private const double MFIBUYMARGIN = 30; // 买入门限
        private const double MFISELLMARGIN = 70; // 卖出门限
    }
}
