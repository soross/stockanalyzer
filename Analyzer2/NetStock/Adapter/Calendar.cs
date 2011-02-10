using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Adapter
{
    class Calendar
    {
        public static DateTime getInstance()
        {
            return DateTime.Now;
        }
    }
}
