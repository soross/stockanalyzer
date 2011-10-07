using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Log
{
    public interface ILogger
    {
        void Log(string info);
    }
}
