using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock.CharMapping;
using FinanceAnalyzer.Log;

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

            PrintResult();
        }

        void AnalyzeNextDay(string stockHistString, string findStockString)
        {
            int prevPos = 0;
            int pos = 0;
            int posNextDayStart = 0;

            while ((pos = stockHistString.IndexOf(findStockString, prevPos)) != -1)
            {
                posNextDayStart = pos + StringMapping_.stockDayStringLength() + 1;

                if ((posNextDayStart + StringMapping_.stockDayStringLength()) >= stockHistString.Length)
                {
                    break;
                }

                string s = stockHistString.Substring(posNextDayStart,
                    StringMapping_.stockDayStringLength());
                FoundedNextDays_.Add(s);

                prevPos = pos + 1;
            }
        }

        void PrintResult()
        {
            foreach (string s in FoundedNextDays_)
            {
                StockRatio ratio = StringMapping_.ParseRatio(s);

                string dbgString = ratio.GetDescription();
                LogMgr.Logger.LogInfo(dbgString);
            }
        }

        StockHistoryCharMapping StringMapping_ = new RatioTenthCharMapping();
        List<string> StockMappings_ = new List<string>();
        List<string> FoundedNextDays_ = new List<string>();
    }
}
