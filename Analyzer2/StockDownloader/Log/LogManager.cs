using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockDownloader.Log
{
    class LogManager : ILogger
    {
        private LogManager()
        {
        }

        public static ILogger GetInstance()
        {
            return instance_;
        }

        public void Log(string info)
        {
            string datetime = DateTime.Now.ToShortTimeString();
            LogListBox_.Items.Add(datetime + ": " + info);
        }

        public ListBox UILog
        {
            set
            {
                LogListBox_ = value;
            }
        }

        ListBox LogListBox_;

        static LogManager instance_ = new LogManager();
    }
}
