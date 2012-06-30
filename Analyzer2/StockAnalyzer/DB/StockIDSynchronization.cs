using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Db.IO;

namespace FinanceAnalyzer.DB
{
    class StockIDSynchronization
    {
        public static void Run()
        {
            StockMongoDB.GetInstance().SynchronizeId();
        }
    }
}
