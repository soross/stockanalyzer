using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntelliName.Log
{
    class UiLog : ILog
    {
        public void Log(string para)
        {
            _Box.Items.Add(para);
        }

        public void SetListBox(ListBox para)
        {
            _Box = para;
        }

        ListBox _Box;
    }
}
