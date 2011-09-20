﻿using System;
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
            throw new NotImplementedException();
        }

        public void Add(StockData data)
        {
            throw new NotImplementedException();
        }

        public void AfterAdd()
        {
            throw new NotImplementedException();
        }

        #endregion

        MongoDatabase DB_;
    }
}