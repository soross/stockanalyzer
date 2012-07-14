using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FinanceAnalyzer.Log
{
    class UITraceLog : UILabelLog
    {
        public override void Log(string val)
        {
            logListBox_.Items.Add(val);

            Debug.WriteLine(val);
        }
    }
}
