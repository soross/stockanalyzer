using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Db.IO;
using Stock.Log;

namespace StockDownloader.Business
{
    class StocksDownloader
    {
        public void Download(List<int> stockIds, DateTime startDate, DateTime endDate)
        {
            StockMongoDbManager mgr = new StockMongoDbManager();
            mgr.InitLog(LogManager.GetInstance());
            mgr.DownloadAllData(stockIds, startDate, endDate);
        }
    }
}
