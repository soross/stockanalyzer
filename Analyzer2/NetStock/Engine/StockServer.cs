using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    public interface StockServer
    {
        Stock getStock(Symbol symbol);
        Stock getStock(Code code);

        // Due to erasure in generic, we are forced to provide two different method
        // name.
        List<Stock> getStocksBySymbols(List<Symbol> symbols);
        List<Stock> getStocksByCodes(List<Code> codes);
        List<Stock> getAllStocks();
    }
}
