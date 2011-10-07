using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockDownloader.Log
{
    interface ILogger
    {
        void Log(string info);
        ListBox UILog
        {
            set;
        }
    }
}
