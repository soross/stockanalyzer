using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace Stock.Common.DataAcquisition
{
    interface IDailyDownloader
    {
        void DownloadData(IStockSaver saver, List<int> stockIds);
        void DownloadData(IStockSaver saver, int stockId, DateTime startDate, DateTime endDate);
    }
}
