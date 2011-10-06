using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.DataAcquisition;

namespace Stock.Db.IO
{
    class StockMongoDbManager
    {
        public void DownloadAllData()
        {
            IDailyDownloader downloader = new ShDailyDownloader();

            StockMongoDBSaver dbsaver = new StockMongoDBSaver();
            dbsaver.OpenDb();

            List<int> stocks = new List<int>{600000, 600001};
            downloader.DownloadData(dbsaver, stocks);
        }
    }
}
