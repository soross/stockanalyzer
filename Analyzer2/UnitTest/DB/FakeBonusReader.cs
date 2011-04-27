using System;
using System.Collections.Generic;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    class FakeBonusReader : IBonusReader
    {
        public IList<Bonus> Query(int StockId)
        {
            Bonus val = new Bonus();
            val.BonusCount = 0.1;
            val.BonusListOn = new DateTime(2009, 1, 4);
            val.BonusYear = 2009;
            val.Dividend = 0.2;
            val.DividendDate = new DateTime(2009, 1, 3);
            val.ExexDividend = new DateTime(2009, 1, 2);
            val.RegistOn = new DateTime(2009, 1, 1);
            val.StockId = STOCKID;

            IList<Bonus> list = new List<Bonus>();
            list.Add(val);
            return list;
        }

        public int Count()
        {
            return 1;
        }

        public const int STOCKID = 111111;
    }
}
