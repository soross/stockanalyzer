using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.DataAcquisition;
using Stock.Log;

namespace Stock.Db.IO
{
    public class StockMongoDbManager
    {
        public void InitLog(ILogger logger)
        {
            Logger_ = logger;
        }

        public void DownloadAllData(List<int> stockIds)
        {
            IDailyDownloader downloader = new ShDailyDownloader();

            StockMongoDBSaver dbsaver = new StockMongoDBSaver();

            Logger_.Log("Open DB finished!");
            //List<int> stocks = new List<int>{600000, 600001};
            downloader.DownloadData(dbsaver, stockIds);
        }

        public void DownloadAllData(List<int> stockIds, DateTime startDate, DateTime endDate)
        {
            IDailyDownloader downloader = new ShDailyDownloader();

            StockMongoDBSaver dbsaver = new StockMongoDBSaver();

            foreach (int id in stockIds)
            {
                Logger_.Log("Download data of stock: " + id);
                downloader.DownloadData(dbsaver, id, startDate, endDate);
            }

            Logger_.Log("Download all data finished!");
        }

        public void LoadFromDB()
        {

        }

        ILogger Logger_;
    }
}
