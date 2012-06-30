using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    /// <summary>
    /// Load all stock information from DB
    /// </summary>
    interface IStockDBReader
    {
        /// <summary>
        /// Load stock's data with specified ID
        /// </summary>
        /// <param name="stockId">Stock ID</param>
        /// <returns>Stock's data</returns>
        IEnumerable<StockData> Load(int stockId);

        /// <summary>
        /// Load all stock IDs
        /// </summary>
        /// <returns>All stock IDs</returns>
        IEnumerable<int> LoadAllIds();
    }
}
