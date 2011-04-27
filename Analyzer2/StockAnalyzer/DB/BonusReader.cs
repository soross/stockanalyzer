using System;
using System.Collections.Generic;
using System.Text;
using IBatisNet.DataMapper;
using System.Collections;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    public interface IBonusReader
    {
        IList<Bonus> Query(int stockId);
        int Count();
    }

    public class BonusReader : IBonusReader
    {
        public void LoadAll()
        {
            _bonusList = _mapper.QueryForList<Bonus>("SelectAll", null);
        }

        public IList<Bonus> Query(int stockId)
        {
            return _mapper.QueryForList<Bonus>("SelectByStockId", stockId);
        }

        public int Count()
        {
            return _bonusList.Count;
        }

        public static void InsertBonus(Bonus bonus)
        {
            _mapper.Insert("InsertBonus", bonus);
        }

        IList<Bonus> _bonusList;
        private static ISqlMapper _mapper = Mapper.Instance(); // iBatis数据库操作
    }
}
