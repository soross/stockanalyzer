using System;
using System.Collections.Generic;
using System.Text;
using IBatisNet.DataMapper;

namespace FinanceAnalyzer.DB
{
    public class StockDBReader
    {
        public void LoadAll()
        {
            _Stocks = _mapper.QueryForList<StockData>("SelectAllStock", null);
        }

        public static IList<StockData> Load(int stockId)
        {
            return _mapper.QueryForList<StockData>("SelectStockByStockId", stockId);
        }

        public static IList<int> LoadId()
        {
            return _mapper.QueryForList<int>("selectItemCount", null);
        }

        public int ItemCount
        {
            get
            {
                return _Stocks.Count;
            }
        }

        public IList<StockData> Stocks
        {
            get
            {
                return _Stocks;
            }
        }

        IList<StockData> _Stocks;

        private static ISqlMapper _mapper = Mapper.Instance(); // iBatis数据库操作
    }
}
