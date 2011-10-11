using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace Stock.Db.IO
{
    public class StockMongoReader
    {
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

        public IEnumerable<StockData> GetStockData(int id)
        {
            return StockMongoDB.GetInstance().GetStock(id);
        }

        List<int> stockIDs_ = new List<int>();
    }
}
