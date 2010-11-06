using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using System.Diagnostics;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Strategy.Indicator
{
    class PriceProp
    {
        public double Price
        {
            get;
            set;
        }
        public bool IsUp
        {
            get;
            set;
        }
    };

    // Reference: http://en.wikipedia.org/wiki/Relative_strength_index 
    class RsiCalculator : IIndicatorCalc
    {
        public string Name
        {
            get { return "RSI"; }
        }

        public void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                StockData stock = hist.GetStock(startDate);
                StockData prevStock = hist.GetPrevDayStock(startDate);
                if ((stock == null) || (prevStock == null))
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                AddTodayPrice(stock.EndPrice, StockJudger.IsRise(stock, prevStock));

                if (_PriceList.Count < RSICALCDAYS)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                Debug.Assert(_PriceList.Count == RSICALCDAYS);

                List<double> emaArr = EmaCalculator.Calc((from item in _PriceList select item.Price).ToList());

                double upSum = 0;
                double allSum = 0;
                for (int i = 0; i < _PriceList.Count; i++ )
                {
                    allSum += emaArr[i];

                    if (_PriceList[i].IsUp)
                    {
                        upSum += emaArr[i];
                    }
                }

                double rsi = 100 * (upSum / allSum);

                _DateIndicators.Add(startDate, rsi); // 前 RSICALCDAYS 天数的RSI计算结果

                startDate = DateFunc.GetNextWorkday(startDate);
            }
        }

        // 得到某天的RSI指标值
        private double GetIndicatorValue(DateTime dt)
        {
            if (_DateIndicators.ContainsKey(dt))
            {
                return _DateIndicators[dt];
            }
            else
            {
                return 0.0; // 默认返回0 
            }
        }

        public OperType MatchSignal(DateTime dt, DateTime prev)
        {
            if (this.GetIndicatorValue(dt) < RSIBUYMARGIN)
            {
                return OperType.Buy;
            }
            else if (this.GetIndicatorValue(dt) > RSISELLMARGIN)
            {
                return OperType.Sell;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        private void AddTodayPrice(double price, bool isUp)
        {
            PriceProp prop = new PriceProp();

            prop.Price = price;
            prop.IsUp = isUp;

            _PriceList.Add(prop);
            if (_PriceList.Count > RSICALCDAYS)
            {
                _PriceList.RemoveAt(0);
            }
        }

        private const double RSIBUYMARGIN = 30; // 买入门限
        private const double RSISELLMARGIN = 70; // 买入门限

        private const int RSICALCDAYS = 14; // RSI计算周期 

        List<PriceProp> _PriceList = new List<PriceProp>();

        private Dictionary<DateTime, double> _DateIndicators = new Dictionary<DateTime, double>();
    }
}
