using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.DataAcquisition
{
    class IDailyDownloader
    {
        void DownloadData(IStockSaver saver, List<int> stockIds);
    }
}
