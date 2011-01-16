using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Display;

namespace FinanceAnalyzer.Display
{
    // »æÖÆ¹ÉÆ±ÇúÏß
    public interface IStockDrawer
    {
        DateTime MinDate { get; }

        DateTime MaxDate { get; }

        StockPoint GetAt(DateTime val);
    }
}
