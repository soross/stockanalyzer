using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace Stock.Common.DataAcquisition
{
    /// <summary>
    /// Download stock data from stock market
    /// 
    /// </summary>
    interface IDailyDownloader
    {
        /// <summary>
        /// Download one week data of stocks
        /// </summary>
        /// <param name="saver">Stock saver</param>
        /// <param name="stockIds">a group of stock id</param>
        void DownloadData(IStockSaver saver, List<int> stockIds);

        /// <summary>
        /// Download a stock data
        /// </summary>
        /// <param name="saver">Stock saver</param>
        /// <param name="stockId">Stock ID</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        void DownloadData(IStockSaver saver, int stockId, DateTime startDate, DateTime endDate);
    }
}
