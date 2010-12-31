using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Business
{
    class FakeIBonusProcessor : IBonusProcessor
    {
        public bool IsExexDividendDate(DateTime date)
        {
            return false;
        }

        public bool IsDividendDate(DateTime date)
        {
            return false;
        }

        public bool IsBonusListOnDate(DateTime date)
        {
            return false;
        }

        public Bonus FindBonus(DateTime date)
        {
            return null;
        }
    }
}
