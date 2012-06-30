using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Stock.Db.IO
{
    /// <summary>
    /// MongoDB Collection item
    /// A item contains all stock ids
    /// </summary>
    class StockIds
    {
        /// <summary>
        /// For MongoDB
        /// </summary>
        public ObjectId Id { get; set; }

        public void AddStockID(int stockId)
        {
            allStockIds_.Add(stockId);
        }

        public void BatchAddStockIDs(IEnumerable<int> ids)
        {
            allStockIds_.AddRange(ids);
        }

        public IEnumerable<int> AllStockID
        {
            get
            {
                return allStockIds_;
            }
            set
            {
                allStockIds_ = value.ToList();
            }
        }
        
        List<int> allStockIds_ = new List<int>();
    }
}
