using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStock.Engine;

namespace DotNetStock.Adapter
{
    class Calendar
    {
        public static SimpleDate getInstance()
        {
            return new SimpleDate();
        }
    }
}
