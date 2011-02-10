using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    interface StockServerFactory
    {
        StockServer getStockServer();
        StockHistoryServer getStockHistoryServer(Code code);
        StockHistoryServer getStockHistoryServer(Code code, Duration duration);
        MarketServer getMarketServer();
    }
}
