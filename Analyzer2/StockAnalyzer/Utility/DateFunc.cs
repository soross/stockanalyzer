using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using FinanceAnalyzer.Utility;

namespace FinanceAnalyzer.Utility
{
    public sealed class DateFunc
    {
        // ǰһ��������
        public static DateTime GetPreviousWorkday(DateTime day)
        {
            day = day.AddDays(-1); 

            while (IsHoliday(day))
            {
                day = day.AddDays(-1);
            }

            return day;
        }

        // ��һ��������
        public static DateTime GetNextWorkday(DateTime day)
        {
            day = day.AddDays(1);

            while (IsHoliday(day))
            {
                day = day.AddDays(1);
            }

            return day;
        }

        // �Ƿ��Ǽ���
        public static bool IsHoliday(DateTime dt)
        {
            return Holidays.Instance().IsHoliday(dt);
            //// ������ĩ
            //if (IsWeekend(dt))
            //{
            //    return true;
            //}

            //// ����ƽ���
            //if ((dt.Day >= 1) && (dt.Day <= 7))
            //{
            //    if ((dt.Month == 5) || (dt.Month == 10))
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        // ����"20080320"���Ƶ��ַ���
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

        // ����"20080320"���Ƶ��ַ���
        public static DateTime ParseString(object param)
        {
            return ParseString(param.ToString());
        }

        private DateFunc()
        {
        }
    }
}
