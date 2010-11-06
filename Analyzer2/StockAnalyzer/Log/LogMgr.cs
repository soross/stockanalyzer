using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer.Log
{
    public sealed class LogMgr
    {
        private LogMgr()
        {
        }

        public static ICustomLog Logger
        {
            get
            {
                return m_Log;
            }
            set
            {
                m_Log = value;
            }
        }

        public static void Close()
        {
            m_Log.Close();
        }

        static ICustomLog m_Log;
    }
}
