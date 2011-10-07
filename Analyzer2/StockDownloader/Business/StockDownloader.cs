using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Db.IO;

namespace StockDownloader.Business
{
    class StocksDownloader
    {
        public void Download(List<int> stockIds)
        {
            StockMongoDbManager mgr = new StockMongoDbManager();
            mgr.DownloadAllData(stockIds);
        }

        public void Download(List<int> stockIds, DateTime startDate, DateTime endDate)
        {
            StockMongoDbManager mgr = new StockMongoDbManager();
            mgr.DownloadAllData(stockIds);
        }
    }
}
