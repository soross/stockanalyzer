using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Stock.Common.Data;
using MongoDB.Bson;

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

        public IEnumerable<StockData> GetStock(int stockId)
        {
            return StockMongoDB.GetInstance().AllStock.Find(new QueryDocument("StockId", stockId));
        }

        public IEnumerable<int> GetAllStockIDs()
        {
            var result = StockMongoDB.GetInstance().AllStock.Distinct("StockId");
            List<int> arr = new List<int>();
            foreach (var item in result)
            {
                arr.Add(item.AsInt32);
            }

            return arr;
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
