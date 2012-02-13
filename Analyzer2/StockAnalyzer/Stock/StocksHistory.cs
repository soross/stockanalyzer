using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    class StocksHistory
    {
        public void Load(IEnumerable<int> stockids)
        {
            IStockDBReader reader = new StockMongoDBReader();
            foreach (int id in stockids)
            {
                StockHistory hist = new StockHistory();
                IEnumerable<StockData> stocks = reader.Load(id);

                hist.InitAllStocks(id, stocks);

                Histories_.Add(hist);
            }            
        }

        public IEnumerable<IStockHistory> GetAllHistories()
        {
            return Histories_;
        }

        public IStockHistory GetHistory(int stockId)
        {
            var result = (from item in Histories_ where item.StockId == stockId select item).FirstOrDefault();
            return result;
        }

        List<IStockHistory> Histories_ = new List<IStockHistory>();
    }
}
