using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    class StockMongoDBReader : IStockDBReader
    {
        #region IStockDBReader Members

        public IList<StockData> Load(int stockId)
        {
            throw new NotImplementedException();
        }

        public IList<int> LoadAllIds()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
