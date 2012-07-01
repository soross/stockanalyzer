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
    public class StockMongoDB
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

        public void InitFindHelper()
        {
            FindHelper_.Init(Collection_);
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

        public void SynchronizeId()
        {
            DB_.DropCollection(DB_ID_COLLECTION);
            var allStockIds = GetAllStockIDs();
            var idColl = DB_.GetCollection(DB_ID_COLLECTION);

            StockIds ids = new StockIds();
            ids.BatchAddStockIDs(allStockIds);
            idColl.Insert(ids);
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
            var idColl = DB_.GetCollection(DB_ID_COLLECTION);
            return idColl.FindOneAs<StockIds>().AllStockID;
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

        StockFindHelper FindHelper_ = new StockFindHelper();

        const string DB_ID_COLLECTION = "ShanghaiID";
    }
}
