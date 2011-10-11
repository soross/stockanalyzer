using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using MongoDB.Driver;

namespace Stock.Db.IO
{
    class StockMongoDBSaver : IStockSaver
    {
        #region IStockSaver Members

        public void BeforeAdd()
        {
        }

        public void Add(StockData data)
        {
            StockMongoDB.GetInstance().AllStock.Insert(data);
        }

        public void AfterAdd()
        {
        }

        #endregion
    }
}
