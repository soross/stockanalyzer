using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Stock.CharMapping
{
    /// <summary>
    /// Convert Stock history to a string for find
    /// </summary>
    abstract class StockHistoryCharMapping
    {
        /// <summary>
        /// Init String map
        /// </summary>
        public StockHistoryCharMapping()
        {
            Init();
        }

        protected abstract void Init();
        protected abstract string GetRatioString(double ratio);
        
        public string GetCharMapping(IStockHistory hist)
        {
            string totalMapping = "";
            DateTime startDate = hist.MinDate;
            DateTime endDate = hist.MaxDate;

            while (startDate < endDate)
            {
                IStockData todayData = hist.GetStock(startDate);
                IStockData pervData = hist.GetPrevDayStock(startDate);

                if (todayData == null)
                {
                    startDate = DateFunc.GetNextWorkday(startDate);
                    continue;
                }

                string s = ParseChars(pervData, todayData);

                if (!string.IsNullOrEmpty(s))
                {
                    totalMapping += s + "|";
                }

                startDate = DateFunc.GetNextWorkday(startDate);
            }

            return totalMapping;
        }

        public string ParseChars(IStockData prevData, IStockData todayData)
        {
            if (todayData == null)
            {
                throw new ArgumentNullException();
            }

            double priseRatio = 0.0;
            if (prevData != null)
            {
                priseRatio = (todayData.StartPrice - prevData.EndPrice) / prevData.EndPrice;
            }
            double upRatio = (todayData.MaxPrice - todayData.StartPrice) / todayData.StartPrice;
            double downRatio = (todayData.MinPrice - todayData.StartPrice) / todayData.StartPrice;
            double endRatio = (todayData.EndPrice - todayData.StartPrice) / todayData.StartPrice;

            string todayMapping = GetRatioString(priseRatio) + GetRatioString(upRatio)
                + GetRatioString(downRatio) + GetRatioString(endRatio);

            return todayMapping;
        }

        protected Dictionary<int, string> RatioToString_ = new Dictionary<int, string>();
    }
}
