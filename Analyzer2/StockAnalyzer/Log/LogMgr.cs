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
                return Log_;
            }
            set
            {
                Log_ = value;
            }
        }

        public static void Close()
        {
            Log_.Close();
        }

        static ICustomLog Log_;
    }
}
