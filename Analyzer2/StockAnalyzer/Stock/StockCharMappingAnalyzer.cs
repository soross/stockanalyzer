using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock.CharMapping;

namespace FinanceAnalyzer.Stock
{
    class StockCharMappingAnalyzer
    {
        public void Init(StocksHistory histories)
        {         
            foreach (IStockHistory hist in histories.GetAllHistories())
            {
                StockMappings_.Add(StringMapping_.GetCharMapping(hist));
            }
        }

        public void FindMatches(IStockHistory hist)
        {
            string s = StringMapping_.GetCharMapping(hist);

            if (string.IsNullOrEmpty(s))
            {
                return;
            }

            var findResult = (from item in StockMappings_ where item.Contains(s) select item);
            foreach (var item in findResult)
            {
                AnalyzeNextDay(item, s);
            }
        }

        void AnalyzeNextDay(string stockHistString, string findStockString)
        {
            int prevPos = 0;
            int pos = 0;
            int posNextDayStart = 0;

            while ((pos = stockHistString.IndexOf(findStockString, prevPos)) != -1)
            {
                posNextDayStart = pos + DAY_STOCK_STR_LENGTH + 1;

                if (posNextDayStart >= stockHistString.Length)
                {
                    break;
                }

                string s = stockHistString.Substring(posNextDayStart, 
                    posNextDayStart + DAY_STOCK_STR_LENGTH);
                FoundedNextDays_.Add(s);

                prevPos = pos + 1;
            }
        }

        StockHistoryCharMapping StringMapping_ = new RatioTenthCharMapping();
        List<string> StockMappings_ = new List<string>();
        List<string> FoundedNextDays_ = new List<string>();

        const int DAY_STOCK_STR_LENGTH = 8;
    }
}
