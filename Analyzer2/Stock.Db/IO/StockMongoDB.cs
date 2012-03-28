using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Stock.Common.Data;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

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
            return AllStock.FindAll();
        }

        /// <summary>
        /// Get stock's information by stock id
        /// </summary>
        /// <param name="stockId">Stock ID, eg. 600012</param>
        /// <returns>stock's information</returns>
        public IEnumerable<StockData> GetStock(int stockId)
        {
            return AllStock.Find(new QueryDocument("StockId", stockId));
        }

        public void AddStockData(StockData sd)
        {
            if (FindHelper_.FindStock(sd.StockId, sd.TradeDate))
            {
                // Already exist
                return;
            }

            AllStock.Insert(sd);
            FindHelper_.AddStockDay(sd.StockId, sd.TradeDate);
        }

        public IEnumerable<int> GetAllStockIDsInDB()
        {
            var result = AllStock.Distinct("StockId");
            List<int> arr = new List<int>();
            foreach (var item in result)
            {
                arr.Add(item.AsInt32);
            }

            return arr;
        }

        public bool FindStockInDB(StockData dt)
        {
            var query = Query.And(
                Query.EQ("StockId", dt.StockId),
                Query.EQ("TradeDate", dt.TradeDate));

            var res = Collection_.Find(query);
            return (res.Count() != 0);
        }

        public IEnumerable<int> GetAllStockIDs()
        {
            return FindHelper_.GetAllStockIDs();
        }

        public bool FindStock(StockData dt)
        {
            return FindHelper_.FindStock(dt.StockId, dt.TradeDate);
        }

        void Init()
        {
            MongoServer server = MongoServer.Create(); // connect to localhost
            DB_ = server.GetDatabase("ChineseStock");
            Collection_ = DB_.GetCollection<StockData>("Shanghai");

            FindHelper_.Init(Collection_);
        }

        static StockMongoDB instance_ = new StockMongoDB();

        MongoCollection<StockData> Collection_;
        MongoDatabase DB_;

        StockFindHelper FindHelper_ = new StockFindHelper();
    }
}
