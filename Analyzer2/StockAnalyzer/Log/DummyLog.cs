using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Log
{
    public class DummyLog : ICustomLog
    {
        public void LogInfo(string msg)
        {
        }

        public void LogInfo(string fmt, object val1)
        {
        }

        public void LogInfo(string fmt, object val1, object val2)
        {
        }

        public void LogInfo(string fmt, object val1, object val2, object val3)
        {
        }

        public void Close()
        {
        }
    }
}
