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
        public void OpenDb()
        {
            MongoServer server = MongoServer.Create(); // connect to localhost
            DB_ = server.GetDatabase("ChineseStock");
        }

        #region IStockSaver Members

        public void BeforeAdd()
        {
        }

        public void Add(StockData data)
        {
            DB_.GetCollection<StockData>("Shanghai").Insert(data);
        }

        public void AfterAdd()
        {
        }

        #endregion

        MongoDatabase DB_;
    }
}
