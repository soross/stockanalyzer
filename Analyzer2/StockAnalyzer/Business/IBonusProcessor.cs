using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Business
{
    /// <summary>
    /// 处理分红送配
    /// </summary>
    public interface IBonusProcessor
    {
        /// <summary>
        /// // 是否是除权除息日
        /// </summary>
        /// <param name="currentDate">日期</param>
        /// <returns>是否是除权除息日</returns>
        bool IsExexDividendDate(DateTime currentDate);
        bool IsDividendDate(DateTime currentDate);
        bool IsBonusListOnDate(DateTime currentDate);
        Bonus FindBonus(DateTime currentDate);
    }
}
