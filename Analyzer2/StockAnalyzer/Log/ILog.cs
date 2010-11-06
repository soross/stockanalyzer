using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Log
{
    public interface ICustomLog
    {
        void LogInfo(string msg);
        void LogInfo(string fmt, object val1);
        void LogInfo(string fmt, object val1, object val2);
        void LogInfo(string fmt, object val1, object val2, object val3);

        void Close();
    }
}
