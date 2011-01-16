using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.DB
{
    public interface IStockSaver
    {
        void BeforeAdd();
        void Add(StockData data);
        void AfterAdd();
    }
}
