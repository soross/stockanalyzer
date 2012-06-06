using System;
using System.Collections.Generic;
using System.Text;
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
        }

        public IList<Bonus> Query(int stockId)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _bonusList.Count;
        }

        public static void InsertBonus(Bonus bonus)
        {
        }

        IList<Bonus> _bonusList;
    }
}
