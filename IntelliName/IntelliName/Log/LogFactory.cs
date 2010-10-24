using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelliName.Log
{
    class LogFactory
    {
        private LogFactory()
        {
        }

        public static LogFactory Instance()
        {
            return _Instance;
        }

        public ILog Log
        {
            get;
            set;
        }

        static LogFactory _Instance = new LogFactory();
    }
}
