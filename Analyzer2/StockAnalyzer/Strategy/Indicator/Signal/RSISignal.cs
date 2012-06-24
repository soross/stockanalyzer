using System.Collections.Generic;
using System.Linq;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    // Reference: http://en.wikipedia.org/wiki/Relative_strength_index 
    class RSISignal : ISignalCalculator
    {

        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }

            prevStock_ = currentStock_;
            currentStock_ = sd;

            if ((prevStock_ == null) || (currentStock_ == null))
            {
                return false;
            }

            AddTodayPrice(sd.EndPrice, StockJudger.IsRise(sd, prevStock_));

            if (Prices_.Count < RSICALCDAYS)
            {
                return false;
            }

            double rsi = CalculateRSI();

            AnalyseOperation(rsi);
            return true;
        }

        private void AnalyseOperation(double rsi)
        {
            if (rsi > RSISELLMARGIN)
            {
                TodayOper_ = OperType.Sell;
            }
            else if (rsi < RSIBUYMARGIN)
            {
                TodayOper_ = OperType.Buy;
            }
            else
            {
                TodayOper_ = OperType.NoOper;
            }
        }

        private double CalculateRSI()
        {
            List<double> emaArr = EmaCalculator.Calc((from item in Prices_ select item.Price).ToList());

            double upSum = 0;
            double allSum = 0;
            for (int i = 0; i < Prices_.Count; i++)
            {
                allSum += emaArr[i];

                if (Prices_[i].IsUp)
                {
                    upSum += emaArr[i];
                }
            }

            double rsi = 100 * (upSum / allSum);
            return rsi;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "RSI";
        }

        #endregion

        private void AddTodayPrice(double price, bool isUp)
        {
            PriceUpDown prop = new PriceUpDown(price, isUp);
            
            Prices_.Add(prop);
            if (Prices_.Count > RSICALCDAYS)
            {
                Prices_.RemoveAt(0);
            }
        }

        IStockData prevStock_ = null;
        IStockData currentStock_ = null;

        OperType TodayOper_;

        List<PriceUpDown> Prices_ = new List<PriceUpDown>();

        private const double RSIBUYMARGIN = 30; // 买入门限
        private const double RSISELLMARGIN = 70; // 买入门限

        private const int RSICALCDAYS = 14; // RSI计算周期 
    }
}
