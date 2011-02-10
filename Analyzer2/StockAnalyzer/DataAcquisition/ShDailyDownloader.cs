using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using DotNetStock.Engine;

namespace FinanceAnalyzer.DataAcquisition
{
    class ShDailyDownloader : IDailyDownloader
    {
        public void DownloadData(IStockSaver saver, List<int> stockIds)
        {
            _stockIds = stockIds;

            List<StockServerFactory> facts = Factories.GetInstance().getStockServerFactories(Country.China);

            if (facts == null)
            {
                return;
            }

            foreach (StockServerFactory fact in facts)
            {
                foreach (int stockId in stockIds)
                {
                    DownloadOneStock(fact, stockId);
                }
            }
        }

        void DownloadOneStock(StockServerFactory fact, int stockId)
        {
            if (!StockMarketChecker.IsChinaShanghaiStock(stockId))
            {
                return;
            }

            Code curCode = Code.newInstance(StockMarketChecker.ToYahooStockId(stockId));

            DateTime prevWeek = DateTime.Now.AddDays(-7);
            Duration duration = new Duration(prevWeek, DateTime.Now);
            StockHistoryServer history = fact.getStockHistoryServer(curCode, duration);

            if (history == null)
            {
                return;
            }


        }

        List<int> _stockIds;
    }
}
