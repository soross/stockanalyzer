using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using Stock.Db.IO;

namespace FinanceAnalyzer.DB
{
    class StockMongoDBReader : IStockDBReader
    {
        #region IStockDBReader Members

        public IEnumerable<StockData> Load(int stockId)
        {
            return Reader_.GetStockData(stockId);
        }

        public IEnumerable<int> LoadAllIds()
        {
            return Reader_.AllIDs;
        }

        #endregion

        StockMongoReader Reader_ = new StockMongoReader();
    }
}
