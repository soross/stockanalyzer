using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Common.Data
{
    public interface IStockSaver
    {
        void BeforeAdd();
        void Add(StockData data);
        void AfterAdd();
    }
}
