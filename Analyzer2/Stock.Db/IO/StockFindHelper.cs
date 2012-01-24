using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Stock.Common.Data;

namespace Stock.Db.IO
{
    class StockFindHelper
    {
        public void Init(MongoCollection<StockData> coll)
        {
            StockIdAndDates_.Clear();

            foreach (var item in coll.FindAll())
            {
                AddStockDay(item.StockId, item.TradeDate);
            }
        }

        public void AddStockDay(int stockId, DateTime dt)
        {
            if (StockIdAndDates_.ContainsKey(stockId))
            {
                StockIdAndDates_[stockId].Add(dt);
            }
            else
            {
                List<DateTime> dates = new List<DateTime>();
                dates.Add(dt);

                StockIdAndDates_.Add(stockId, dates);
            }
        }

        public bool FindStock(int stockId, DateTime dt)
        {
            if (!StockIdAndDates_.ContainsKey(stockId))
            {
                return false;
            }

            return StockIdAndDates_[stockId].Contains(dt);
        }

        public IEnumerable<int> GetAllStockIDs()
        {
            return StockIdAndDates_.Keys;
        }

        Dictionary<int, List<DateTime>> StockIdAndDates_ = new Dictionary<int,List<DateTime>>();
    }
}
