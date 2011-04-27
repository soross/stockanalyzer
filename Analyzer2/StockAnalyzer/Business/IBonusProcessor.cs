using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business
{
    public interface IBonusProcessor
    {
        bool IsExexDividendDate(DateTime currentDate);
        bool IsDividendDate(DateTime currentDate);
        bool IsBonusListOnDate(DateTime currentDate);
        Bonus FindBonus(DateTime currentDate);
    }
}
