using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace Stock.Db.IO
{
    /// <summary>
    /// Read stock information from MongoDB
    /// </summary>
    public class StockMongoReader
    {
        public StockMongoReader()
        {
            Load();
        }

        public void Load()
        {
            stockIDs_.Clear();
            stockIDs_.AddRange(StockMongoDB.GetInstance().GetAllStockIDs());
        }

        public ICollection<int> AllIDs
        {
            get
            {
                return stockIDs_;
            }
        }

        /// <summary>
        /// Get stock's information by stock id
        /// </summary>
        /// <param name="id">Stock ID, eg. 600012</param>
        /// <returns>stock's information</returns>
        public IEnumerable<StockData> GetStockData(int id)
        {
            return StockMongoDB.GetInstance().GetStock(id);
        }

        List<int> stockIDs_ = new List<int>();
    }
}
