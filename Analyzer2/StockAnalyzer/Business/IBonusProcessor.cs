using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;

namespace FinanceAnalyzer.Business
{
    public interface IBonusProcessor
    {
        bool IsExexDividendDate(DateTime date);
        bool IsDividendDate(DateTime date);
        bool IsBonusListOnDate(DateTime date);
        Bonus FindBonus(DateTime date);
    }
}
