using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.KDJ
{
    class KdjCalculator : IKdjCalculator
    {
        public void Calc(IStockHistory hist)
        {
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;
            DateTime yesterday = startDate;

            int curPos = 1;
            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                if (stock == null)
                {
                    yesterday = startDate;
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                MaxPrices_.AddLast(stock.MaxPrice);
                MinPrices_.AddLast(stock.MinPrice);

                if (curPos <= _DAYS)
                {
                    _Storage.SetD(startDate, 50);
                    _Storage.SetK(startDate, 50);
                }
                else
                {
                    MaxPrices_.RemoveFirst();
                    MinPrices_.RemoveFirst();

                    List<double> maxPriceList = new List<double>();
                    List<double> minPriceList = new List<double>();
                    maxPriceList.AddRange(MaxPrices_);
                    minPriceList.AddRange(MinPrices_);

                    minPriceList.Sort();
                    maxPriceList.Sort();

                    double minPriceOfNDays = minPriceList[0]; 
                    double maxPriceOfNDays = maxPriceList[maxPriceList.Count - 1]; // 最后一个
                    double rsv = (stock.EndPrice - minPriceOfNDays) * 100 / (maxPriceOfNDays - minPriceOfNDays);

                    double k = (1.0 / 3) * rsv + (2.0 / 3) * _Storage.GetK(yesterday);
                    double d = (1.0 / 3) * k + (2.0 / 3) * _Storage.GetD(yesterday);

                    _Storage.SetD(startDate, d);
                    _Storage.SetK(startDate, k);

                    //LogMgr.Logger.LogInfo("Current Day: " + startDate.ToShortDateString() 
                    //    + ", D = " + d + ", K = " + k);
                }

                yesterday = startDate;
                startDate = DateFunc.GetNextWorkday(startDate);
                curPos++;
            }
        }

        public IKdjStorage GetStorage()
        {
            return _Storage;
        }

        KdjStorage _Storage = new KdjStorage();

        private const int _DAYS = 9;
        
        private LinkedList<double> MaxPrices_ = new LinkedList<double>();
        private LinkedList<double> MinPrices_ = new LinkedList<double>();
    }
}
