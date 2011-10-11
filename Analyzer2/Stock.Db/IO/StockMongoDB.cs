using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Stock.Common.Data;

namespace Stock.Db.IO
{
    class StockMongoDB
    {
        private StockMongoDB()
        {
            Init();
        }

        public static StockMongoDB GetInstance()
        {
            return instance_;
        }

        public MongoCollection<StockData> AllStock
        {
            get
            {
                return Collection_;
            }
        }

        public IEnumerable<StockData> GetAllStocks()
        {
            return StockMongoDB.GetInstance().AllStock.FindAll();
        }

        void Init()
        {
            MongoServer server = MongoServer.Create(); // connect to localhost
            DB_ = server.GetDatabase("ChineseStock");
            Collection_ = DB_.GetCollection<StockData>("Shanghai");
        }

        static StockMongoDB instance_ = new StockMongoDB();

        MongoCollection<StockData> Collection_;
        MongoDatabase DB_;
    }
}
