using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace Stock.Db.IO
{
    class StockMongoReader
    {
        public void Load()
        {
            foreach (var item in StockMongoDB.GetInstance().GetAllStocks())
            {
                stockIDs_.Add(item.StockId);
            }
        }

        public ICollection<int> AllIDs
        {
            get
            {
                return stockIDs_;
            }
        }

        public List<StockData> GetStockData(int id)
        {
            return null;
        }

        HashSet<int> stockIDs_ = new HashSet<int>();
    }
}
