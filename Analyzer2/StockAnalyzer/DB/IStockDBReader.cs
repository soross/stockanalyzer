using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.DB
{
    interface IStockDBReader
    {
        IEnumerable<StockData> Load(int stockId);
        IEnumerable<int> LoadAllIds();
    }
}
