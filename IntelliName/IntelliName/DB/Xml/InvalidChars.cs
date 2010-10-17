using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.DB.Xml
{
    // xml 无法识别的字符集合 
    class InvalidChars
    {
        public static bool IsValidChar(char para)
        {
            return (_InvalidChars.IndexOf(para) == -1);
        }

        private const string _InvalidChars = "?";
    }
}
