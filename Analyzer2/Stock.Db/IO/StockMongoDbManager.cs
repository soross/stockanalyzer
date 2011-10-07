using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.DataAcquisition;

namespace Stock.Db.IO
{
    public class StockMongoDbManager
    {
        public void DownloadAllData(List<int> stockIds)
        {
            IDailyDownloader downloader = new ShDailyDownloader();

            StockMongoDBSaver dbsaver = new StockMongoDBSaver();
            dbsaver.OpenDb();

            //List<int> stocks = new List<int>{600000, 600001};
            downloader.DownloadData(dbsaver, stockIds);
        }

        public void DownloadAllData(List<int> stockIds, DateTime startDate, DateTime endDate)
        {
            IDailyDownloader downloader = new ShDailyDownloader();

            StockMongoDBSaver dbsaver = new StockMongoDBSaver();
            dbsaver.OpenDb();

            foreach (int id in stockIds)
            {
                downloader.DownloadData(dbsaver, id, startDate, endDate);
            }
        }
    }
}
