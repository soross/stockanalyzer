using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class StockHistoryNotFoundException : Exception
    {
        public StockHistoryNotFoundException()
        {
        }

        public StockHistoryNotFoundException(string description)
        {
        }
    }
}
