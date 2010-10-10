using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.DB.Xml
{
    class InvalidChars
    {
        public static bool IsValidChar(char para)
        {
            return (_InvalidChars.IndexOf(para) == -1);
        }

        private const string _InvalidChars = "?";
    }
}
