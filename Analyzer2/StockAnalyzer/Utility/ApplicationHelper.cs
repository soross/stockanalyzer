using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinanceAnalyzer.Utility
{
    class ApplicationHelper
    {
        public static string GetAppPath()
        {
            // Get application path
            string applicationPath = Application.ExecutablePath;
            int slashIndex = applicationPath.LastIndexOf('\\');
            applicationPath = applicationPath.Substring(0, slashIndex);

            return applicationPath;
        }
    }
}
