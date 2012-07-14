using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FinanceAnalyzer.Log;
using System.Globalization;

namespace FinanceAnalyzer.Log
{
    // 记录日志到界面label控件
    class UILabelLog : ICustomLog
    {
        public virtual void Log(string val)
        {
            logListBox_.Items.Add(val);
        }

        public void LogInfo(string msg)
        {
            DateTime cur = DateTime.Now;
            Log(cur.ToString("T", CultureInfo.CurrentCulture) + " : " + msg);
        }

        public void LogInfo(string fmt, object val1)
        {
            DateTime cur = DateTime.Now;
            string str = string.Format(CultureInfo.CurrentCulture, fmt, val1);
            Log(cur.ToString("T", CultureInfo.CurrentCulture) + " : " + str);
        }

        public void LogInfo(string fmt, object val1, object val2)
        {
            DateTime cur = DateTime.Now;
            string str = string.Format(CultureInfo.CurrentCulture, fmt, val1, val2);
            Log(cur.ToString("T", CultureInfo.CurrentCulture) + " : " + str);
        }

        public void LogInfo(string fmt, object val1, object val2, object val3)
        {
            DateTime cur = DateTime.Now;
            string str = string.Format(CultureInfo.CurrentCulture, fmt, val1, val2, val3);
            Log(cur.ToString("T", CultureInfo.CurrentCulture) + " : " + str);
        }

        public ListBox LogListBox
        {
            set
            {
                logListBox_ = value;
            }
        }

        public virtual void Close()
        {

        }

        protected ListBox logListBox_;
    }
}
