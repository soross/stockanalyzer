using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FinanceAnalyzer.Log;

namespace FinanceAnalyzer.Log
{
    // 同时记录界面和文件
    class UIFileLog : UILabelLog
    {
        public override void Log(string val)
        {
            _logListBox.Items.Add(val);

            _log.Debug(val);
        }

        public override void Close()
        {
        }

        // Create a logger for use in this class
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
