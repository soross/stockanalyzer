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

            KdStorage_.MinDate = hist.MinDate;
            KdStorage_.MaxDate = hist.MaxDate;

            double prevK = DEFAULT_KD_VALUE;
            double prevD = DEFAULT_KD_VALUE;

            int curPos = 1;
            while (startDate < endDate)
            {
                IStockData stock = hist.GetStock(startDate);
                if (stock == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                MaxPrices_.AddLast(stock.MaxPrice);
                MinPrices_.AddLast(stock.MinPrice);

                if (curPos <= KD_CALC_DAYS)
                {
                    KdStorage_.SetD(startDate, DEFAULT_KD_VALUE);
                    prevD = DEFAULT_KD_VALUE;

                    KdStorage_.SetK(startDate, DEFAULT_KD_VALUE);
                    prevK = DEFAULT_KD_VALUE;
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

                    double k = (1.0 / 3) * rsv + (2.0 / 3) * prevK;
                    double d = (1.0 / 3) * k + (2.0 / 3) * prevD;

                    KdStorage_.SetD(startDate, d);
                    KdStorage_.SetK(startDate, k);

                    prevK = k;
                    prevD = d;
                }

                startDate = DateFunc.GetNextWorkday(startDate);
                curPos++;
            }
        }

        public IKdjStorage GetStorage()
        {
            return KdStorage_;
        }

        KdjStorage KdStorage_ = new KdjStorage();

        private const int KD_CALC_DAYS = 9;
        
        private LinkedList<double> MaxPrices_ = new LinkedList<double>();
        private LinkedList<double> MinPrices_ = new LinkedList<double>();

        const double DEFAULT_KD_VALUE = 50;
    }
}
