using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business
{
    public class BonusProcessor : IBonusProcessor
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
        public bool IsExexDividendDate(DateTime currentDate)
        {
            return _ExexDividendDate.Contains(currentDate);
        }

        // 是否是分红日
        public bool IsDividendDate(DateTime currentDate)
        {
            return _DividendDate.Contains(currentDate);
        }

        // 是否是红股上市日
        public bool IsBonusListOnDate(DateTime currentDate)
        {
            return _BonusListOnDate.Contains(currentDate);
        }

        public Bonus FindBonus(DateTime currentDate)
        {
            if (_DateToBonus.ContainsKey(currentDate))
            {
                return _DateToBonus[currentDate];
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
