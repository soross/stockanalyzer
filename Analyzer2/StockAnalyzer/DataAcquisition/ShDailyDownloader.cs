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
            StockHistoryServer history = fact.getStockHistoryServer(this.code, duration);
        }

        List<int> _stockIds;
    }
}
