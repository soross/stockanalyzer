using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class StockNotFoundException : Exception
    {
        public StockNotFoundException(string desc)
        {
        }

        public StockNotFoundException(string desc, Exception ex)
        {
        }

        public StockNotFoundException()
        {
        }
    }
}
