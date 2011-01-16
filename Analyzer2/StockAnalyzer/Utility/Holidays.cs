using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    class Holidays
    {
        static Holidays _Instance = new Holidays();

        public static Holidays Instance()
        {
            return _Instance;
        }

        public bool IsHoliday(DateTime dt)
        {
            if (IsWeekend(dt))
            {
                return true;
            }

            if (_AllHolidays.Contains(dt))
            {
                return true;
            }

            return false;
        }

        // 是否是周末
        public static bool IsWeekend(DateTime dt)
        {
            return (dt.DayOfWeek == DayOfWeek.Saturday) || (dt.DayOfWeek == DayOfWeek.Sunday);
        }

        private Holidays()
        {
            Init();
        }

        private void Init()
        {
            int CurYear = 2010;
            AddDate(CurYear, 1, 1); // 元旦
            AddDateRange(CurYear, 2, 13, 19); // 2010年春节
            AddDate(CurYear, 4, 5); // 清明节
            AddDate(CurYear, 5, 3); // 劳动节
            AddDateRange(CurYear, 6, 14, 16); // 端午节
            AddDateRange(CurYear, 9, 22, 24); // 中秋节
            AddDateRange(CurYear, 10, 1, 7); // 国庆节

            CurYear--;
            AddDateRange(CurYear, 1, 1, 3); // 2009元旦
            AddDateRange(CurYear, 1, 25, 31); // 春节
            AddDateRange(CurYear, 4, 4, 6); // 清明节
            AddDateRange(CurYear, 5, 1, 3); // 劳动节
            AddDateRange(CurYear, 5, 28, 30); // 端午节
            AddDateRange(CurYear, 10, 1, 8); // 国庆节

            CurYear--;
            AddDate(CurYear-1, 12, 31);
            AddDate(CurYear, 1, 1); // 2008元旦
            AddDateRange(CurYear, 2, 6, 12); // 春节
            AddDateRange(CurYear, 4, 4, 6); // 清明节
            AddDateRange(CurYear, 5, 1, 3); // 劳动节
            AddDateRange(CurYear, 6, 7, 9); // 端午节
            AddDateRange(CurYear, 9, 13, 15); // 中秋节
            AddDateRange(CurYear, 9, 29, 10, 5); // 国庆节

            CurYear--;
            AddDateRange(CurYear, 1, 1, 3); // 2007元旦
            AddDateRange(CurYear, 2, 17, 25); // 春节
            AddDateRange(CurYear, 5, 1, 7); // 劳动节
            AddDateRange(CurYear, 10, 1, 7); // 国庆节

            CurYear--;
            AddDateRange(CurYear, 1, 1, 3); // 2006元旦
            AddDateRange(CurYear, 1, 26, 2, 5); // 春节
            AddDateRange(CurYear, 5, 1, 7); // 劳动节
            AddDateRange(CurYear, 10, 1, 8); // 国庆节

            CurYear--;
            AddDateRange(CurYear, 5, 1, 7); // 劳动节
            AddDateRange(CurYear, 10, 1, 9); // 国庆节

            CurYear--;
            AddDate(CurYear, 1, 1); // 2004元旦
            AddDateRange(CurYear, 1, 19, 28); // 春节
            AddDateRange(CurYear, 5, 1, 7); // 劳动节
            AddDateRange(CurYear, 10, 1, 7); // 国庆节

            CurYear--;
            AddDate(CurYear, 1, 1); // 2003元旦
            AddDateRange(CurYear, 1, 30, 2, 7); // 春节
            AddDateRange(CurYear, 5, 1, 9); // 2003劳动节
            AddDateRange(CurYear, 10, 1, 7); // 国庆节

            // 以下为推测
            CurYear--;
            AddDateRange(CurYear, 1, 1, 3); // 2002元旦
            AddDateRange(CurYear, 2, 11, 22); // 春节
            AddDateRange(CurYear, 5, 1, 7); // 劳动节
            AddDateRange(CurYear, 9, 30, 10, 7); // 国庆节

            CurYear--;
            AddDate(CurYear, 1, 1); // 2001元旦
            AddDateRange(CurYear, 1, 22, 2, 2); // 春节
            AddDateRange(CurYear, 5, 1, 7); // 劳动节
            AddDateRange(CurYear, 10, 1, 5); // 国庆节

            CurYear--;
            AddDate(CurYear, 1, 3); // 2000元旦
            AddDateRange(CurYear, 1, 31, 2, 11); // 春节
            AddDateRange(CurYear, 5, 1, 5); // 劳动节
            AddDateRange(CurYear, 10, 2, 6); // 国庆节

            CurYear--;
            AddDate(CurYear, 1, 1); // 1999元旦
            AddDateRange(CurYear, 2, 10, 26); // 春节
            AddDate(CurYear, 5, 3); // 劳动节
            AddDateRange(CurYear, 10, 1, 7); // 国庆节
            AddDate(CurYear, 12, 31); // 2000年元旦

            AddDateRange(1991, 2, 14, 21); // 91年春节
        }

        private void AddDate(int year, int month, int day)
        {
            _AllHolidays.Add(new DateTime(year, month, day));
        }

        private void AddDateRange(int year, int month, int day1, int day2)
        {
            for (int i = day1; i < day2; i++)
            {
                AddDate(year, month, i);
            }
        }

        private void AddDateRange(int year, int month1, int day1, int month2, int day2)
        {
            DateTime startDate = new DateTime(year, month1, day1);
            DateTime endDate = new DateTime(year, month2, day2);

            while (startDate <= endDate)
            {
                _AllHolidays.Add(startDate);

                startDate = startDate.AddDays(1);
            }
        }

        List<DateTime> _AllHolidays = new List<DateTime>();
    }
}
