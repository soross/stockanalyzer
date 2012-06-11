using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    // 参考：http://en.wikipedia.org/wiki/MACD
    // 传统MACD计算方式 
    class MACDAlgorithm
    {
        public bool AddValue(double val)
        {
            LongPrices_.Add(val);
            if (LongPrices_.Count > LONGDAYS)
            {
                LongPrices_.RemoveAt(0);
            }

            ShortPrices_.Add(val);
            if (ShortPrices_.Count > SHORTDAYS)
            {
                ShortPrices_.RemoveAt(0);
            }

            if (!IsCountEnough())
            {
                return false;
            }

            // 计算长周期和短周期均值
            double longEMA = LongPrices_.Average();
            double shortEMA = ShortPrices_.Average();

            double diff = shortEMA - longEMA; // 短周期均值-长周期均值

            DiffPrices_.Add(diff);
            if (DiffPrices_.Count < MIDDAYS)
            {
                return false;
            }
            else if (DiffPrices_.Count > MIDDAYS)
            {
                DiffPrices_.RemoveAt(0);
            }
            Debug.Assert(DiffPrices_.Count == MIDDAYS);

            double dea = DiffPrices_.Average(); // DIFF 均线

            macdValue_ = diff - dea; // DIFF与均线之差
            return true;
        }

        public double GetMacd()
        {
            return macdValue_;
        }

        private bool IsCountEnough()
        {
            return (LongPrices_.Count >= LONGDAYS) && (ShortPrices_.Count >= SHORTDAYS);
        }

        double macdValue_ = 0.0;

        List<double> LongPrices_ = new List<double>();
        List<double> ShortPrices_ = new List<double>();
        List<double> DiffPrices_ = new List<double>();

        private const int SHORTDAYS = 12;
        private const int LONGDAYS = 26;
        private const int MIDDAYS = 9; // 计算DIFF的平均线 
    }
}
