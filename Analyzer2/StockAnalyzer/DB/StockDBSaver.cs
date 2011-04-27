using System;
using System.Collections.Generic;
using System.Text;
using IBatisNet.DataMapper;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    // 保存股票信息到数据库 
    public class StockDBSaver : IStockSaver
    {
        public void BeforeAdd()
        {
            _Session = _mapper.BeginTransaction();
        }

        public void Add(StockData data)
        {
            if (_Session != null)
            {
                _Session.SqlMapper.Insert("InsertStock", data);
            }
            else
            {
                _mapper.Insert("InsertStock", data);
            }
        }

        public void AfterAdd()
        {
            _mapper.CommitTransaction();
            _Session = null;
        }

        private ISqlMapSession _Session; // 批量插入使用

        private static ISqlMapper _mapper = Mapper.Instance(); // iBatis数据库操作
    }
}
