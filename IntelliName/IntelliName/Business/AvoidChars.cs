using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.Business
{
    class AvoidChars
    {
        public bool IsAvoidChars(char c1)
        {
            return AvoidChars.Contains(c1);
        }

        char[] AvoidChars
        {
            get;
            set;
        }
    }
}
