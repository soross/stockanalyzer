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
            DBSaver_.OpenDb();

            foreach (var item in DBSaver_.GetAllStocks())
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

        StockMongoDBSaver DBSaver_ = new StockMongoDBSaver();
        HashSet<int> stockIDs_ = new HashSet<int>();
    }
}
