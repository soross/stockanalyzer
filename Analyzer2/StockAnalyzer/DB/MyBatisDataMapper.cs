using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBatis.DataMapper;
using MyBatis.DataMapper.Configuration;

namespace FinanceAnalyzer.DB
{
    class MyBatisDataMapper
    {
        public MyBatisDataMapper()
        {
            IConfigurationEngine engine = new DefaultConfigurationEngine();
            IMapperFactory factory = engine.BuildMapperFactory();
            _mapper = ((IDataMapperAccessor)factory).DataMapper;
        }

        public static IDataMapper GetMapper()
        {
            return _mapper;
        }

        private static IDataMapper _mapper = null; // iBatis数据库操作
    }
}
