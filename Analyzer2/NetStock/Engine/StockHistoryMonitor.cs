using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    class StockHistoryMonitor
    {
        /** Creates a new instance of StockHistoryMonitor */
        public StockHistoryMonitor(int nThreads)
        {
            // Default database size is 10.
            //this(nThreads, 10);
        }

        public bool setStockServerFactories(List<StockServerFactory> factories)
        {
            _factories = factories;

            return true;
        }

        List<StockServerFactory> _factories = new List<StockServerFactory>();
    }
}
