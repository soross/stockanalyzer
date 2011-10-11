using System;
using System.Collections.Generic;
using System.Text;
using MyBatis.DataMapper;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    public class StockDBReader : IStockDBReader
    {
        public StockDBReader(IDataMapper mapper)
        {
            _mapper = mapper;
        }

        public void LoadAll()
        {
            _Stocks = _mapper.QueryForList<StockData>("SelectAllStock", null);
        }

        /// <summary>
        /// 获取某一只股票的数据
        /// </summary>
        /// <param name="stockId">Stock ID</param>
        /// <returns>某一只股票的数据</returns>
        public IList<StockData> Load(int stockId)
        {
            return _mapper.QueryForList<StockData>("SelectStockByStockId", stockId);
        }

        /// <summary>
        /// 获取数据库中所有的Stock ID
        /// </summary>
        /// <returns>数据库中所有的Stock ID</returns>
        public IList<int> LoadAllIds()
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

        private IDataMapper _mapper; // iBatis数据库操作
    }
}
