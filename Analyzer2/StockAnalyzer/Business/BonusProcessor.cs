using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Business
{
    public class BonusProcessor
    {
        public void Load(int stockId, IBonusReader reader)
        {
            _Bonuses = reader.Query(stockId);

            foreach (Bonus val in _Bonuses)
            {
                _BonusListOnDate.Add(val.BonusListOn);
                _DividendDate.Add(val.DividendDate);
                _ExexDividendDate.Add(val.ExexDividend);

                _DateToBonus.Add(val.ExexDividend, val);
            }
        }

        // 是否是除权除息日
        public Boolean IsExexDividendDate(DateTime date)
        {
            return _ExexDividendDate.Contains(date);
        }

        // 是否是分红日
        public Boolean IsDividendDate(DateTime date)
        {
            return _DividendDate.Contains(date);
        }

        // 是否是红股上市日
        public Boolean IsBonusListOnDate(DateTime date)
        {
            return _BonusListOnDate.Contains(date);
        }

        public Bonus FindBonus(DateTime date)
        {
            if (_DateToBonus.ContainsKey(date))
            {
                return _DateToBonus[date];
            }
            else
            {
                return null;
            }
        }

        IList<Bonus> _Bonuses;
        IList<DateTime> _ExexDividendDate = new List<DateTime>();
        IList<DateTime> _DividendDate = new List<DateTime>();
        IList<DateTime> _BonusListOnDate = new List<DateTime>();

        Dictionary<DateTime, Bonus> _DateToBonus = new Dictionary<DateTime, Bonus>();
    }
}
