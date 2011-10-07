using System;
using System.Collections.Generic;
using System.Text;
using MyBatis.DataMapper;
using Stock.Common.Data;
using MyBatis.DataMapper.Configuration;
using MyBatis.DataMapper.Session;

namespace FinanceAnalyzer.DB
{
    // 保存股票信息到数据库 
    public class StockDBSaver : IStockSaver
    {
        public void BeforeAdd()
        {
            _mapper = MyBatisDataMapper.GetMapper();
            ISessionFactory sessionFactory = ((IModelStoreAccessor)_mapper).ModelStore.SessionFactory;
        }

        public void Add(StockData data)
        {
            _mapper.Insert("InsertStock", data);
        }

        public void AfterAdd()
        {
        }
        
        private static IDataMapper _mapper = null; // iBatis数据库操作
    }
}
