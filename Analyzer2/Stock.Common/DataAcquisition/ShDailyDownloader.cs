using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using DotNetStock.Engine;

namespace Stock.Common.DataAcquisition
{
    public class ShDailyDownloader : IDailyDownloader
    {
        public void DownloadData(IStockSaver saver, List<int> stockIds)
        {
            _stockIds = stockIds;
            StockSaver_ = saver;

            List<StockServerFactory> facts = Factories.GetInstance().getStockServerFactories(Country.China);

            if (facts == null)
            {
                return;
            }

            DateTime prevWeek = DateTime.Now.AddDays(-7);

            foreach (StockServerFactory fact in facts)
            {
                foreach (int stockId in stockIds)
                {
                    DownloadOneStock(fact, stockId, prevWeek, DateTime.Now);
                }
            }
        }

        public void DownloadData(IStockSaver saver, int stockId, DateTime startDate, DateTime endDate)
        {
            StockSaver_ = saver;
            List<StockServerFactory> facts = Factories.GetInstance().getStockServerFactories(Country.China);

            if (facts == null)
            {
                return;
            }

            foreach (StockServerFactory fact in facts)
            {
                DownloadOneStock(fact, stockId, startDate, endDate);
            }
        }

        void DownloadOneStock(StockServerFactory fact, int stockId, DateTime startDate, DateTime endDate)
        {
            if (!StockMarketChecker.IsChinaShanghaiStock(stockId))
            {
                return;
            }

            Code curCode = Code.newInstance(StockMarketChecker.ToYahooStockId(stockId));

            Duration duration = new Duration(startDate, endDate);
            StockHistoryServer history = fact.getStockHistoryServer(curCode, duration);

            if (history == null)
            {
                return;
            }

            int numberOfDate = history.getNumOfCalendar();

            for (int i = 0; i < numberOfDate; i++)
            {
                SimpleDate dt = history.getCalendar(i);

                DotNetStock.Engine.Stock stock = history.getStock(dt);

                StockData data = StockDataAdapter.ToStockData(stock);

                StockSaver_.Add(data);
            }
        }

        List<int> _stockIds;

        IStockSaver StockSaver_;
    }
}
