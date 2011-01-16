using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

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

                _MaxPrices.AddLast(stock.MaxPrice);
                _MinPrices.AddLast(stock.MinPrice);
                _MaxPriceList.Add(stock.MaxPrice);
                _MinPriceList.Add(stock.MinPrice);

                if (curPos <= _DAYS)
                {
                    _Storage.SetD(startDate, 50);
                    _Storage.SetK(startDate, 50);
                }
                else
                {
                    _MaxPriceList.Remove(_MaxPrices.First.Value);
                    _MinPriceList.Remove(_MinPrices.First.Value);
                    _MaxPrices.RemoveFirst();
                    _MinPrices.RemoveFirst();

                    _MinPriceList.Sort();
                    _MaxPriceList.Sort();

                    double minPriceOfNDays = _MinPriceList[0]; 
                    double maxPriceOfNDays = _MaxPriceList[_MaxPriceList.Count - 1]; // 最后一个
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

        List<double> _MaxPriceList = new List<double>();
        List<double> _MinPriceList = new List<double>();

        private LinkedList<double> _MaxPrices = new LinkedList<double>();
        private LinkedList<double> _MinPrices = new LinkedList<double>();
    }
}
