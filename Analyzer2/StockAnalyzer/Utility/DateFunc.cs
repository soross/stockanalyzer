using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Utility
{
    /// <summary>
    /// Some useful date and time functions
    /// </summary>
    public sealed class DateFunc
    {
        // 前一个工作日
        public static DateTime GetPreviousWorkday(DateTime day)
        {
            day = day.AddDays(-1); 

            while (IsHoliday(day))
            {
                day = day.AddDays(-1);
            }

            return day;
        }

        // 下一个工作日
        public static DateTime GetNextWorkday(DateTime day)
        {
            day = day.AddDays(1);

            while (IsHoliday(day))
            {
                day = day.AddDays(1);
            }

            return day;
        }

        // 是否是假期
        public static bool IsHoliday(DateTime dt)
        {
            return Holidays.Instance().IsHoliday(dt);
        }

        // 处理"20080320"类似的字符串
        public static DateTime ParseString(string str)
        {
            if ((string.IsNullOrEmpty(str)) || str.Length != 8)
            {
                throw new ArgumentException("Length error!");
            }

            int year = int.Parse(str.Substring(0, 4), CultureInfo.CurrentCulture);
            int month = int.Parse(str.Substring(4, 2), CultureInfo.CurrentCulture);
            int day = int.Parse(str.Substring(6), CultureInfo.CurrentCulture);
            Debug.Assert(year > 1990 && (month > 0) && (month <= 12) && (day >0) && (day <= 31));

            DateTime time = new DateTime(year, month, day);
            return time;
        }

        // 处理"20080320"类似的字符串
        public static DateTime ParseString(object param)
        {
            return ParseString(param.ToString());
        }

        /// <summary>
        /// Convert time to UTC time
        /// </summary>
        /// <param name="dt">Local time</param>
        /// <returns>UTC Time</returns>
        public static DateTime ConvertToUTC(DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.Local);
        }

        /// <summary>
        /// Convert time to Local time
        /// </summary>
        /// <param name="dt">UTC time</param>
        /// <returns>Local Time</returns>
        public static DateTime ConvertToLocal(DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.Local);
        }

        private DateFunc()
        {
        }
    }
}
