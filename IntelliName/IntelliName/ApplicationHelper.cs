﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntelliName
{
    class ApplicationHelper
    {
        // 得到应用程序所在路径，不包含最后的\\ 
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
