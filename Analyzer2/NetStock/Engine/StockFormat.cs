using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStock.Engine
{
    interface StockFormat
    {
        List<Stock> parse(String source);
    }
}
